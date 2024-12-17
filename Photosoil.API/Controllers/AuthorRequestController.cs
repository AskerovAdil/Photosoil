using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorRequestController : ControllerBase
    {
        private readonly AuthorRequestService _authorService;
        public AuthorRequestController(AuthorRequestService authorService) {
            _authorService = authorService;
        }

        [HttpGet(nameof(GetAll))]
        [Authorize]
        public IActionResult GetAll()
        {
            var response= _authorService.Get();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _authorService.Get(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        } 


        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] AuthorRequest authorRequest)
        {
            var response = await _authorService.Post(authorRequest);
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
