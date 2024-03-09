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
    public class PublicationController : ControllerBase
    {
        private readonly PublicationService _publicationService;
        public PublicationController(PublicationService publicationService) {
            _publicationService = publicationService;
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

        [HttpPost(nameof(Post))]
        [ProducesResponseType(typeof(PublicationVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] PublicationVM publicationVm)
        {
            var response = await _publicationService.Post(publicationVm);

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
