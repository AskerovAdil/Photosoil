using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel;
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
        [HttpPost(nameof(SyncOrderTerm))]
        public async Task<IActionResult> SyncOrderTerm()
        {
            var response = await _classificationService.SyncOrderTerm();

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] ClassificationVM classification)
        {
            var response = await _classificationService.Post(classification);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        //[HttpPut(nameof(Put) + "/{Id}")]
        //public async Task<IActionResult> Put(int Id, [FromForm] string? NameRu, [FromForm] string? NameEng, [FromForm] TranslationMode TranslationMode = TranslationMode.Neutral)
        //{
        //    var response = await _classificationService.Put(Id, NameRu,NameEng,TranslationMode );
        //
        //    return response.Error ? BadRequest(response) : Ok(response);
        //}
        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] ClassificationVM classification)
        {
            var response = await _classificationService.PutData(Id, classification);

            return response.Error ? BadRequest(response) : Ok(response);
        }


        /// <summary>
        /// Обновить порядковые номера Классификатора
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        [HttpPost(nameof(UpdateOrder))]
        public async Task<IActionResult> UpdateOrder(List<OrderVM> orders)
        {
            var response = await _classificationService.UpdateOrder(orders);

            return response.Error ? BadRequest() : Ok();
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _classificationService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
