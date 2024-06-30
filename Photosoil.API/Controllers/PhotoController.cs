using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PhotoController: ControllerBase
    {
        private readonly PhotoService _photoService;
        public PhotoController(PhotoService soilObject) {
            _photoService = soilObject;
        }
        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response = _photoService.GetAll();

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int Id)
        {
            var response = _photoService.GetById(Id);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpGet(nameof(GetBySoilId))]
        public IActionResult GetBySoilId(int soilId)
        {
            var response= _photoService.GetBySoilId(soilId);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post([FromForm] PhotoVM photo)
        {
            var response = await _photoService.Post(photo);

            return response.Error ? BadRequest(response) : Ok(response);
        }
        [HttpPut(nameof(Put) + "/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromForm] string? TitleEng, [FromForm] string? TitleRu)
        {
            var response = await _photoService.Put(Id, TitleEng,TitleRu);

            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int photoId)
        {
            var response = _photoService.Delete(photoId);

            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
