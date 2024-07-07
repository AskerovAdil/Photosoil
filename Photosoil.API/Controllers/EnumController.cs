using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Service.Abstract;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly IEnumService _enumService;
        public EnumController(IEnumService enumService) {
            _enumService = enumService;
        }

        [HttpGet(nameof(SoilObjects))]
        public IActionResult SoilObjects()
        {
            var props = _enumService.GetSoilObjectNames();
            return Ok(props);
        }
        [HttpGet(nameof(PublicationType))]
        public IActionResult PublicationType()
        {
            var props = _enumService.GetPublicationNames();
            return Ok(props);
        }

        [HttpGet(nameof(TranslationMode))]
        public IActionResult TranslationMode()
        {
            var props = _enumService.GetTranslationMode();
            return Ok(props);
        }
    }
}
