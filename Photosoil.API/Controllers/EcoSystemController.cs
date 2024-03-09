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
    public class EcoSystemController : ControllerBase
    {
        private readonly EcoSystemService _ecoSystemService;
        public EcoSystemController(EcoSystemService ecoSystemService) {
            _ecoSystemService = ecoSystemService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response= _ecoSystemService.GetAll();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _ecoSystemService.GetById(Id);
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        [ProducesResponseType(typeof(EcoSystemVM), StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] EcoSystemVM ecoSystemVm)
        {
            var response = await _ecoSystemService.Post(ecoSystemVm);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(Put) + "/Id")]
        public async Task<IActionResult> Put(int Id, [FromForm] EcoSystemVM soilObject)
        {
            var response = await _ecoSystemService.Put(Id, soilObject);
            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _ecoSystemService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

    }
}
