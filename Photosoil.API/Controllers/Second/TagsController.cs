using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using Photosoil.Service.Services;
using Photosoil.Service.Services.Second;

namespace Photosoil.API.Controllers.Second
{
    [ApiExplorerSettings(GroupName = "Second")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagsService _tagsService;
        public TagsController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response = _tagsService.GetAll();

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromForm] string? NameRu, [FromForm] string? NameEng)
        {
            var response = await _tagsService.Post(NameRu, NameEng);

            return response.Error ? BadRequest(response) : Ok(response);
        }


        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromForm] string? NameRu, [FromForm] string? NameEng)
        {
            var response = await _tagsService.Put(Id, NameRu, NameEng);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _tagsService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
