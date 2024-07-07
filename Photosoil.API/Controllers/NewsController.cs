using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Services;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [Route("api/News")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet(nameof(GetById) + "/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = _newsService.GetById(Id);
            return Ok(response);
        }
  
        [HttpPost("Post")]
        public async Task<IActionResult> Post(News news )
        {
            _newsService.Post(news);
            return Ok();
        }


    }
}
