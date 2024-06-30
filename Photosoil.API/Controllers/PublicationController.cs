using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly PublicationService _publicationService;
        public PublicationController(PublicationService publicationService) {
            _publicationService = publicationService;
        }

        [HttpGet(nameof(GetAdminAll))]
        [Authorize]
        public IActionResult GetAdminAll()
        {
            string? userId = User.FindFirstValue("Id");
            string? role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            int.TryParse(userId, out var id);

            var response = _publicationService.GetAll(id,role);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response= _publicationService.GetAll();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _publicationService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPut(nameof(Put) + "/{Id}")]
        [ProducesResponseType(typeof(PublicationVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int Id,[FromForm] PublicationVM publicationVm)
        {
            var response = await _publicationService.Put(Id, publicationVm);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(PutVisible) + "/{Id}")]
        public async Task<IActionResult> PutVisible(int Id, [FromForm] bool isVisible)
        {

            var response = await _publicationService.PutVisible(Id, isVisible);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetForUpdate))]
        public IActionResult GetForUpdate(int Id)
        {
            var response = _publicationService.GetForUpdate(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }


        [HttpPost(nameof(Post))]
        [Authorize]
        [ProducesResponseType(typeof(PublicationVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(List<PublicationVM> publicationVm)
        {
            string? userId = User.FindFirstValue("Id");
            int.TryParse(userId, out var id);

            var response = await _publicationService.Post(id, publicationVm);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _publicationService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
