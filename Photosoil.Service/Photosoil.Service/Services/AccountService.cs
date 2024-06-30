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

namespace Photosoil.Service.Services
{
    // AccountService.cs
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ServiceResponse> Delete(int Id)
        {
            var existingUser = _context.User.FirstOrDefault(x => x.Id== Id);

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");

            await _userManager.DeleteAsync(existingUser);
            return ServiceResponse<ApplicationUser>.OkResponse(existingUser);
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


            return ServiceResponse<ApplicationUser>.OkResponse(existingUser);
        }
        public async Task<ServiceResponse> GetAll()
        {
            var existingUser = await _userManager.Users.ToListAsync();

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");

            return ServiceResponse<IList<ApplicationUser>>.OkResponse(existingUser);
        }

        public async Task<ServiceResponse> GetById(int UserId)
        {
            var existingUser =  _userManager.Users.Include(x=>x.SoilObjects)
                .Include(x=>x.EcoSystems).Include(x=>x.Publications).Include(x=>x.Authors)
                .ThenInclude(x=>x.DataEng)
                .Include(x=>x.Authors)
                .ThenInclude(x=>x.DataRu)
                .FirstOrDefault(x=>x.Id == UserId);

            if (existingUser == null)
                return ServiceResponse.BadResponse("Not Found");

            return ServiceResponse<ApplicationUser>.OkResponse(existingUser);
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

            user.RefreshToken = response.RefreshToken;
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
            var refreshToken = GenerateRefreshToken();
            var second = (deadTime - DateTime.Now).TotalSeconds;

            var response = new AuthResponse()
            {
                Token = encodedJwt,
                RefreshToken = refreshToken,
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
