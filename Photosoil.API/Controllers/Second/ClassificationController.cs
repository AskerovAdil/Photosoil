using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using Photosoil.Service.Services.Second;

namespace Photosoil.API.Controllers.Second
{
    [ApiExplorerSettings(GroupName = "Second")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly ClassificationService _classificationService;
        public ClassificationController(ClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response = _classificationService.GetAll();

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var response = _classificationService.GetById(id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromForm] ClassificationVM classification)
        {
            var response = await _classificationService.Post(classification);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _classificationService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
