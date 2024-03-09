using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class SoilObjectController : ControllerBase
    {
        private readonly ISoilObjectService _soilObjectService;
        public SoilObjectController(ISoilObjectService soilObject) {
            _soilObjectService = soilObject;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response= _soilObjectService.Get();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _soilObjectService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpGet(nameof(GetByType))]
        public IActionResult GetByType(SoilObjectType soilType)
        {
            var response = _soilObjectService.GetByType(soilType);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPost(nameof(GetByFilter))]
        public IActionResult GetByFilter([FromForm] params int[] ids)
        {
            var response = _soilObjectService.GetByFilter(ids);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPost(nameof(Post))]
        [ProducesResponseType(typeof(SoilObjectVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] SoilObjectVM soilObject)
        {
            var response = await _soilObjectService.Post(soilObject);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(PostMass))]
        [ProducesResponseType(typeof(SoilObjectVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostMass([FromForm] SoilMass soilMass)
        {
            var response = await _soilObjectService.PostMass(soilMass);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPut(nameof(Put)+"/Id")]
        public async Task<IActionResult> Put(int Id,[FromForm]SoilObjectVM soilObject)
        {
            var response = await _soilObjectService.Put(Id,soilObject);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _soilObjectService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
