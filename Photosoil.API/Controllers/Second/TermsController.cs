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
        [HttpPost(nameof(Put))]
        public Task<IActionResult> Put([FromForm] TermsVM term)
        {
            var response = _termsService.Put(term);

            return Task.FromResult<IActionResult>(response.Error ? BadRequest(response) : Ok(response));
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            var response = _termsService.Delete(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
