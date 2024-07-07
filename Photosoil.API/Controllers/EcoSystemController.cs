using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class EcoSystemController : ControllerBase
    {
        private readonly EcoSystemService _ecoSystemService;
        public EcoSystemController(EcoSystemService ecoSystemService) {
            _ecoSystemService = ecoSystemService;
        }

        [HttpGet(nameof(GetAdminAll))]
        [Authorize]
        public IActionResult GetAdminAll()
        {
            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);

            var response = _ecoSystemService.GetAdminAll(id, role);

            return response.Error ? BadRequest(response) : Ok(response);
        }



        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {

            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);
            var response= _ecoSystemService.GetAll(id,role);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _ecoSystemService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetForUpdate))]
        public IActionResult GetForUpdate(int Id)
        {
            var response = _ecoSystemService.GetForUpdate(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        [Authorize]
        [ProducesResponseType(typeof(List<EcoSystemVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] EcoSystemVM ecoSystemVm)
        {
            string? userId = User.FindFirstValue("Id");
            int.TryParse(userId, out var id);

            var response = await _ecoSystemService.Post(id, ecoSystemVm);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, EcoSystemVM soilObject)
        {
            var response = await _ecoSystemService.Put(Id, soilObject);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(PutVisible) + "/{Id}")]
        public async Task<IActionResult> PutVisible(int Id, [FromForm] bool isVisible)
        {

            var response = await _ecoSystemService.PutVisible(Id, isVisible);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int TranslationId)
        {
            var response = _ecoSystemService.Delete(TranslationId);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
