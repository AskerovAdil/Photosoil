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
                        .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
                        .content {{ padding: 20px; border: 1px solid #ddd; }}
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
                            <p>Если вы не запрашивали сброс пароля, пожалуйста, немедленно свяжитесь с администратором системы.</p>
                            <p>С уважением,<br>Команда Photosoil</p>
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
