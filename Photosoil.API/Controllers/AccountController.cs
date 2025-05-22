using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var response = await _accountService.GetAll();
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById) + "/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _accountService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPost("ChangeRole")]
        public async Task<IActionResult> ChangeRole(int Id, bool isAdmin = false)
        {
            var response = await _accountService.MakeAdmin(Id, isAdmin);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            var response = await _accountService.RegisterUserAsync(model);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] string refreshToken)
        {
            var response = await _accountService.RefreshTokenAsync(refreshToken);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel    model)
        {
            var response = await _accountService.AuthenticateUserAsync(model);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _accountService.Delete(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        /// <summary>
        /// Сброс пароля - генерирует новый пароль и отправляет его на email пользователя
        /// </summary>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var response = await _accountService.ResetPasswordAsync(model);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        /// <summary>
        /// Изменение пароля пользователя (требуется ввод текущего пароля)
        /// </summary>
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            // Получаем email пользователя из токена
            var email = User.Identity.Name;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Error = true, Message = "Пользователь не аутентифицирован" });
            }

            var response = await _accountService.ChangePasswordAsync(email, model);
            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
