using Microsoft.AspNetCore.Mvc;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;

namespace Photosoil.API.Controllers
{
    [Route("api/become-author")]
    [ApiController]
    public class BecomeAuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public BecomeAuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Отправка заявки на роль автора
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> BecomeAuthor([FromBody] BecomeAuthorRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authorService.BecomeAuthorAsync(model);
            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}