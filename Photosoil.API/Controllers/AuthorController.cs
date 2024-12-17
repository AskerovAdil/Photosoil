using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Security.Claims;

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

        [HttpGet(nameof(GetAdminAll))]
        [Authorize]
        public IActionResult GetAdminAll()
        {
            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);

            var response = _authorService.Get(id, role);

            return response.Error ? BadRequest(response) : Ok(response);
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
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AuthorVM authorVM)
        {
            var userId = User.FindFirstValue("Id");
            int.TryParse(userId, out var id);

            var response = await _authorService.Post(id, authorVM);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id,[FromBody] AuthorVM authorVM)
        {
            var response = await _authorService.Put(Id, authorVM);

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
