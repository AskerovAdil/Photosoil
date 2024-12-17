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
    public class TermsController : ControllerBase
    {
        private readonly TermsService _termsService;
        public TermsController(TermsService termsService)
        {
            _termsService = termsService;
        }

        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromForm] TermsVM term)
        {
            var response = await _termsService.Post(term);

            return response.Error ? BadRequest(response) : Ok(response);
        }


        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromForm] string NameRu, [FromForm] string NameEng)
        {
            var response = await _termsService.Put(Id, NameRu, NameEng);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _termsService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
