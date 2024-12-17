using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Models;
using Photosoil.Service.Services;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {

            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);

            var response = _newsService.GetAll(id,role);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpGet(nameof(GetAdminAll))]
        [Authorize]
        public IActionResult GetAdminAll()
        {
            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);

            var response = _newsService.GetAdminAll(id,     role);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _newsService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetForUpdate))]
        public IActionResult GetForUpdate(int Id)
        {
            var response = _newsService.GetForUpdate(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        [Authorize]
        [ProducesResponseType(typeof(List<NewsVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] NewsVM newsVM)
        {
            string? userId = User.FindFirstValue("Id");
            int.TryParse(userId, out var id);

            var response = await _newsService.Post(id, newsVM);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, NewsVM newsVM)
        {
            var response = await _newsService.Put(Id, newsVM);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPut(nameof(PutVisible) + "/{Id}")]
        public async Task<IActionResult> PutVisible(int Id, [FromForm] bool isVisible)
        {
            var response = await _newsService.PutVisible(Id, isVisible);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int TranslationId)
        {
            var response = _newsService.Delete(TranslationId);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
