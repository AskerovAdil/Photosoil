using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService) {
            _authorService = authorService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response= _authorService.Get();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _authorService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }


        [HttpPost(nameof(Post))]
        [ProducesResponseType(typeof(AuthorVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] AuthorVM authorVM)
        {
            var response = await _authorService.Post(authorVM);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _authorService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
