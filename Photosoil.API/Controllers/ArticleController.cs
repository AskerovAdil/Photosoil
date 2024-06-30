using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Security.Claims;
using System.Security.Principal;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        public ArticleController(ArticleService articleService) {
            _articleService = articleService;
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var verificationKey = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var response = _articleService.GetAll();
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _articleService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        [ProducesResponseType(typeof(SoilObjectVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ArticleVM articleVM)
        {
            var response = await _articleService.Post(articleVM);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _articleService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
