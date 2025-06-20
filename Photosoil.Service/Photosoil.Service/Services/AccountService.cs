using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Photosoil.Core.Models;
using Microsoft.EntityFrameworkCore;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Response;
using Photosoil.Service.Data;
using Newtonsoft.Json.Linq;
using AutoMapper;
using Photosoil.Service.Helpers.ViewModel.Request;

namespace Photosoil.Service.Services
{
    // AccountService.cs
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountService(
            IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context,
            EmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> Delete(int Id)
        {
            var existingUser = _context.User.FirstOrDefault(x => x.Id== Id);

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");

            await _userManager.DeleteAsync(existingUser);

            var response = _mapper.Map<AccountResponse>(existingUser);
            return ServiceResponse<AccountResponse>.OkResponse(response);
        }
        public async Task<ServiceResponse> MakeAdmin(int Id, bool isAdmin)
        {
            var existingUser = _context.User.FirstOrDefault(x => x.Id == Id);
            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");

            if(isAdmin)
                existingUser.Role = "Admin";
            else
                existingUser.Role = "Moderator";

            await _userManager.UpdateAsync(existingUser);

            var response = _mapper.Map<AccountResponse>(existingUser);

            return ServiceResponse<AccountResponse>.OkResponse(response);
        }
        public async Task<ServiceResponse> GetAll()
        {
            var existingUser = await _userManager.Users.ToListAsync();

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");
            var response = _mapper.Map<IList<AccountResponse>>(existingUser);

            return ServiceResponse<IList<AccountResponse>>.OkResponse(response);
        }

        public async Task<ServiceResponse> GetById(int UserId)
        {
            var existingUser =  _userManager.Users.Include(x=>x.SoilObjects)
                .ThenInclude(x=>x.Translations)
                .Include(x=>x.EcoSystems)
                .ThenInclude(x=>x.Translations)
                .Include(x=>x.Publications)
                .ThenInclude(x => x.Translations)

                .Include(x=>x.Authors)
                .ThenInclude(x=>x.DataEng)
                .Include(x=>x.Authors)
                .ThenInclude(x=>x.DataRu)
                .Include(x=>x.News)
                .ThenInclude(x=>x.Translations)
                .FirstOrDefault(x=>x.Id == UserId);

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");
            var response = _mapper.Map<AccountResponse>(existingUser);

            return ServiceResponse<AccountResponse>.OkResponse(response);
        }



        public async Task<ServiceResponse> RegisterUserAsync(RegisterViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                throw new Exception("User with this email already exists.");
            
            var refreshToken = GenerateRefreshToken();
            var user = new ApplicationUser { Name = model.Name,UserName = model.Email, Email = model.Email, RefreshToken = refreshToken, Role = "Moderator"};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var response = GenerateToken(user);
                response.RefreshToken = refreshToken;

                return ServiceResponse<AuthResponse>.OkResponse(response);
            }

            return ServiceResponse.BadResponse("");
        }

        public async Task<ServiceResponse<AuthResponse>> AuthenticateUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            var response = GenerateToken(user);
            if(user.RefreshToken != null)
                response.RefreshToken = user.RefreshToken;
            else
            {
                user.RefreshToken = GenerateRefreshToken();
                response.RefreshToken = user.RefreshToken;
            }

            await _userManager.UpdateAsync(user);
            return ServiceResponse<AuthResponse>.OkResponse(response);
        }

        private AuthResponse GenerateToken(ApplicationUser user)
        {

            var claims = new List<Claim> 
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)

            };
            // создаем JWT-токен

            var deadTime = DateTime.Now.Add(TimeSpan.FromMinutes(60));

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: deadTime,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //var refreshToken = GenerateRefreshToken();
            var second = (deadTime - DateTime.Now).TotalSeconds;

            var response = new AuthResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name.ToString(),
                Role = user.Role,
                Token = encodedJwt,
                //RefreshToken = refreshToken,
                DeadTime = second.ToString()
            };


            return response;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<ServiceResponse<AuthResponse>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
                return ServiceResponse<AuthResponse>.BadResponse("Invalid refresh token.");

            var response = GenerateToken(user);
            response.RefreshToken = refreshToken;

            return ServiceResponse<AuthResponse>.OkResponse(response);
        }

        public async Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ServiceResponse.BadResponse("Пользователь с таким email не найден.");

            // Генерация нового случайного пароля
            string newPassword = GenerateRandomPassword();
            
            // Сброс пароля пользователя
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ServiceResponse.BadResponse(string.Join(", ", errors));
            }

            // Формирование HTML-сообщения с новым паролем
            var message = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ color: white; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .password {{ font-family: monospace; font-size: 18px; background-color: #f5f5f5; padding: 10px; border: 1px solid #ddd; margin: 10px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Новый пароль для доступа к системе Photosoil</h2>
                        </div>
                        <div class='content'>
                            <p>Здравствуйте, {user.Name}!</p>
                            <p>Для вашей учетной записи в системе Photosoil был сгенерирован новый пароль:</p>
                            <div class='password'>{newPassword}</div>
                            <p>Рекомендуем изменить этот пароль после входа в систему.</p>
                            <p>Если вы не запрашивали сброс пароля, пожалуйста, немедленно свяжитесь с администратором системы.</p>
                            <m>С уважением,<br>Команда Photosoil</m>
                        </div>
                    </div>
                </body>
                </html>";

            // Отправка email с новым паролем
            await _emailService.SendEmailAsync(
                model.Email,
                "Новый пароль для доступа к системе Photosoil",
                message);

            return ServiceResponse.OkResponse;
        }
        
        /// <summary>
        /// Изменение пароля пользователя (требуется ввод текущего пароля)
        /// </summary>
        public async Task<ServiceResponse> ChangePasswordAsync(string email, ChangePasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ServiceResponse.BadResponse("Пользователь не найден.");

            // Проверка текущего пароля
            if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
                return ServiceResponse.BadResponse("Текущий пароль указан неверно.");

            // Проверка, что новый пароль отличается от текущего
            if (model.CurrentPassword == model.NewPassword)
                return ServiceResponse.BadResponse("Новый пароль должен отличаться от текущего.");

            // Изменение пароля
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ServiceResponse.BadResponse(string.Join(", ", errors));
            }

            // Отправка уведомления о смене пароля
            var message = $@"
                <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html dir='ltr' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' lang='ru'>
 <head>
  <meta charset='UTF-8'>
  <meta content='width=device-width, initial-scale=1' name='viewport'>
  <meta name='x-apple-disable-message-reformatting'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <meta content='telephone=no' name='format-detection'>
  <title>Новое письмо</title><!--[if (mso 16)]>
    <style type='text/css'>
    a {{text-decoration: none;}}
    </style>
    <![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>
<noscript>
         <xml>
           <o:OfficeDocumentSettings>
           <o:AllowPNG></o:AllowPNG>
           <o:PixelsPerInch>96</o:PixelsPerInch>
           </o:OfficeDocumentSettings>
         </xml>
      </noscript>
<![endif]--><!--[if mso]><xml>
    <w:WordDocument xmlns:w='urn:schemas-microsoft-com:office:word'>
      <w:DontUseAdvancedTypographyReadingMail/>
    </w:WordDocument>
    </xml><![endif]-->
  <style type='text/css'>
.rollover:hover .rollover-first {{
  max-height:0px!important;
  display:none!important;
}}
.rollover:hover .rollover-second {{
  max-height:none!important;
  display:block!important;
}}
.rollover span {{
  font-size:0px;
}}
u + .body img ~ div div {{
  display:none;
}}
#outlook a {{
  padding:0;
}}
span.MsoHyperlink,
span.MsoHyperlinkFollowed {{
  color:inherit;
  mso-style-priority:99;
}}
a.es-button {{
  mso-style-priority:100!important;
  text-decoration:none!important;
}}
a[x-apple-data-detectors],
#MessageViewBody a {{
  color:inherit!important;
  text-decoration:none!important;
  font-size:inherit!important;
  font-family:inherit!important;
  font-weight:inherit!important;
  line-height:inherit!important;
}}
.es-desk-hidden {{
  display:none;
  float:left;
  overflow:hidden;
  width:0;
  max-height:0;
  line-height:0;
  mso-hide:all;
}}
@media only screen and (max-width:600px) {{.es-p-default {{ }} *[class='gmail-fix'] {{ display:none!important }} p, a {{ line-height:150%!important }} h1, h1 a {{ line-height:120%!important }} h2, h2 a {{ line-height:120%!important }} h3, h3 a {{ line-height:120%!important }} h4, h4 a {{ line-height:120%!important }} h5, h5 a {{ line-height:120%!important }} h6, h6 a {{ line-height:120%!important }} .es-header-body p {{ }} .es-content-body p {{ }} .es-footer-body p {{ }} .es-infoblock p {{ }} h1 {{ font-size:36px!important; text-align:left }} h2 {{ font-size:26px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} h4 {{ font-size:24px!important; text-align:left }} h5 {{ font-size:20px!important; text-align:left }} h6 {{ font-size:16px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:36px!important }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:26px!important }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important }} .es-header-body h4 a, .es-content-body h4 a, .es-footer-body h4 a {{ font-size:24px!important }} .es-header-body h5 a, .es-content-body h5 a, .es-footer-body h5 a {{ font-size:20px!important }} .es-header-body h6 a, .es-content-body h6 a, .es-footer-body h6 a {{ font-size:16px!important }} .es-menu td a {{ font-size:12px!important }} .es-header-body p, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock a {{ font-size:12px!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3, .es-m-txt-c h4, .es-m-txt-c h5, .es-m-txt-c h6 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3, .es-m-txt-r h4, .es-m-txt-r h5, .es-m-txt-r h6 {{ text-align:right!important }} .es-m-txt-j, .es-m-txt-j h1, .es-m-txt-j h2, .es-m-txt-j h3, .es-m-txt-j h4, .es-m-txt-j h5, .es-m-txt-j h6 {{ text-align:justify!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3, .es-m-txt-l h4, .es-m-txt-l h5, .es-m-txt-l h6 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-m-txt-r .rollover:hover .rollover-second, .es-m-txt-c .rollover:hover .rollover-second, .es-m-txt-l .rollover:hover .rollover-second {{ display:inline!important }} .es-m-txt-r .rollover span, .es-m-txt-c .rollover span, .es-m-txt-l .rollover span {{ line-height:0!important; font-size:0!important; display:block }} .es-spacer {{ display:inline-table }} a.es-button, button.es-button {{ font-size:20px!important; padding:10px 20px 10px 20px!important; line-height:120%!important }} a.es-button, button.es-button, .es-button-border {{ display:inline-block!important }} .es-m-fw, .es-m-fw.es-fw, .es-m-fw .es-button {{ display:block!important }} .es-m-il, .es-m-il .es-button, .es-social, .es-social td, .es-menu {{ display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} .es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} .h-auto {{ height:auto!important }} }}
@media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}
</style>
 </head>
 <body class='body' style='width:100%;height:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'>
  <div dir='ltr' class='es-wrapper-color' lang='ru' style='background-color:#FAFAFA'><!--[if gte mso 9]>
			<v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'>
				<v:fill type='tile' color='#fafafa'></v:fill>
			</v:background>
		<![endif]-->
   <table width='100%' cellspacing='0' cellpadding='0' class='es-wrapper' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA'>
     <tr>
      <td valign='top' style='padding:0;Margin:0'>
       <table cellspacing='0' cellpadding='0' align='center' background class='es-header' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important;background-color:transparent;background-repeat:repeat;background-position:center top'>
         <tr>
          <td align='center' bgcolor='transparent' style='padding:0;Margin:0'>
           <table cellpadding='0' cellspacing='0' bgcolor='#ffffff' align='center' class='es-header-body' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'>
             <tr>
              <td align='left' style='padding:0;Margin:0;padding-top:20px;padding-right:20px;padding-left:20px'>
               <table width='100%' cellpadding='0' cellspacing='0' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                 <tr>
                  <td align='left' style='padding:0;Margin:0;width:560px'>
                   <table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                     <tr>
                      <td align='center' style='padding:0;Margin:0;padding-top:20px;padding-bottom:20px;font-size:0'><img src='https://downloader.disk.yandex.ru/preview/0188678f02d69ca0bfa82a2972f0e03507d772a286e02b04a96f72c296f745f9/683f5682/zhqnZw0pxDglg7aOcf-gfmZWrJdziQfhKRW6sfrH1YBl50MWYyWvMgPs0qJwNbKJUlLmZ1ACN2G-JL7LWjXgkQ%3D%3D?uid=0&amp;filename=logo.png&amp;disposition=inline&amp;hash=&amp;limit=0&amp;content_type=image%2Fjpeg&amp;owner_uid=0&amp;tknv=v3&amp;size=2088x1251' alt='' height='60' referrerpolicy='no-referrer' class='adapt-img' style='display:block;font-size:14px;border:0;outline:none;text-decoration:none'></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding='0' cellspacing='0' align='center' class='es-content' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important'>
         <tr>
          <td align='center' style='padding:0;Margin:0'>
           <table bgcolor='#ffffff' align='center' cellpadding='0' cellspacing='0' class='es-content-body' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;border-radius:10px;background-color:#FFFFFF;width:600px'>
             <tr>
              <td align='left' style='padding:30px;Margin:0'>
               <table cellpadding='0' cellspacing='0' width='100%' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                 <tr>
                  <td align='center' valign='top' style='padding:0;Margin:0;width:540px'>
                   <table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:15px;padding-bottom:15px'><h2 style='Margin:0;font-family:arial, 'helvetica neue', helvetica, sans-serif;mso-line-height-rule:exactly;letter-spacing:0;font-size:26px;font-style:normal;font-weight:bold;line-height:31.2px;color:#333333'>Новый пароль для доступа к системе&nbsp;</h2></td>
                     </tr>
                     <tr>
                      <td align='center' style='padding:0;Margin:0;font-size:0'>
                       <table border='0' width='100%' height='100%' cellpadding='0' cellspacing='0' class='es-spacer' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                         <tr>
                          <td style='padding:0;Margin:0;border-bottom:1px solid #cccccc;background:none;height:0px;width:100%;margin:0px'></td>
                         </tr>
                       </table></td>
                     </tr>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:20px'><p style='Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'><strong> </strong> Здравствуйте, {{user.Name}}! <strong> </strong></p></td>
                     </tr>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:5px;padding-bottom:10px'><p style='Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'>Для вашей учетной записи в системе Photosoil был сгенерирован новый пароль:</p></td>
                     </tr>
                     <tr>
                      <td align='left' bgcolor='#efefef' style='padding:10px;Margin:0'><p style='Margin:0;mso-line-height-rule:exactly;font-family:'courier new', courier, 'lucida sans typewriter', 'lucida typewriter', monospace;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'>{{newPassword}}</p></td>
                     </tr>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:10px'><p style='Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'>Рекомендуем изменить этот пароль после входа в систему.</p></td>
                     </tr>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:5px'><p style='Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'>Если вы не запрашивали сброс пароля, пожалуйста, немедленно свяжитесь с администратором системы.</p></td>
                     </tr>
                     <tr>
                      <td align='left' style='padding:0;Margin:0;padding-top:10px'><p style='Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px'><strong> </strong><em>С уважением,<br>Команда Photosoil</em><strong> </strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding='0' cellspacing='0' align='center' class='es-footer' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important;background-color:transparent;background-repeat:repeat;background-position:center top'>
         <tr>
          <td align='center' style='padding:0;Margin:0'>
           <table align='center' cellpadding='0' cellspacing='0' class='es-footer-body' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px' role='none'>
             <tr>
              <td align='left' style='Margin:0;padding-top:20px;padding-right:20px;padding-left:20px;padding-bottom:20px'>
               <table cellpadding='0' cellspacing='0' width='100%' role='none' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                 <tr>
                  <td align='left' style='padding:0;Margin:0;width:560px'>
                   <table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                     <tr>
                      <td align='center' style='padding:0;Margin:0;font-size:0'>
                       <table cellpadding='0' cellspacing='0' class='es-table-not-adapt es-social' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                         <tr>
                          <td align='center' valign='top' style='padding:0;Margin:0;padding-right:20px'><a target='_blank' href='https://t.me/Photosoil' style='mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px'><img title='Telegram' src='https://ekxqldb.stripocdn.email/content/assets/img/messenger-icons/logo-gray/telegram-logo-gray.png' alt='Telegram' width='32' height='32' style='display:block;font-size:14px;border:0;outline:none;text-decoration:none'></a></td>
                          <td align='center' valign='top' style='padding:0;Margin:0'><a target='_blank' href='https://www.youtube.com/@photosoil' style='mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px'><img title='Youtube' src='https://ekxqldb.stripocdn.email/content/assets/img/social-icons/logo-gray/youtube-logo-gray.png' alt='Yt' width='32' height='32' style='display:block;font-size:14px;border:0;outline:none;text-decoration:none'></a></td>
                         </tr>
                       </table></td>
                     </tr>
                     <tr>
                      <td style='padding:0;Margin:0;font-size:0'>
                       <table cellpadding='0' cellspacing='0' width='100%' class='es-menu' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>
                         <tr class='links'>
                          <td align='center' valign='top' width='100.00%' style='padding:0;Margin:0;border:0;padding-bottom:10px;padding-top:10px'>
                           <div style='vertical-align:middle;display:block'><a target='_blank' href='https://46.173.25.120:3000/ru' style='mso-line-height-rule:exactly;text-decoration:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;display:block;color:#333333;font-size:12px'>photosoil.tsu.ru</a>
                           </div></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table></td>
     </tr>
   </table>
  </div>
 </body>
</html>";

            // Отправка email с уведомлением о смене пароля
            await _emailService.SendEmailAsync(
                user.Email,
                "Пароль успешно изменен - Photosoil",
                message);

            return ServiceResponse.OkResponse;
        }
        
        /// <summary>
        /// Генерирует случайный пароль, соответствующий требованиям безопасности
        /// </summary>
        private string GenerateRandomPassword()
        {
            // Набор символов для генерации пароля
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            
            // Создаем генератор случайных чисел
            var random = new Random();
            var passwordBuilder = new StringBuilder();
            
            // Добавляем минимум 1 символ из каждой категории для соответствия требованиям безопасности
            passwordBuilder.Append(lowerChars[random.Next(lowerChars.Length)]);
            passwordBuilder.Append(upperChars[random.Next(upperChars.Length)]);
            passwordBuilder.Append(numbers[random.Next(numbers.Length)]);
            passwordBuilder.Append(specialChars[random.Next(specialChars.Length)]);
            
            // Добавляем еще символы до достижения минимальной длины (12 символов)
            const int minLength = 12;
            const string allChars = lowerChars + upperChars + numbers + specialChars;
            
            while (passwordBuilder.Length < minLength)
            {
                passwordBuilder.Append(allChars[random.Next(allChars.Length)]);
            }
            
            // Перемешиваем символы в пароле
            char[] passwordArray = passwordBuilder.ToString().ToCharArray();
            int n = passwordArray.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = passwordArray[k];
                passwordArray[k] = passwordArray[n];
                passwordArray[n] = value;
            }
            
            return new string(passwordArray);
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password{ get; set; }
    }
}
