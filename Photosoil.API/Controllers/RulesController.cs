using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Photosoil.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Main")]
    [Route("api/[controller]")]
    [ApiController]

    public class RulesController : ControllerBase
    {
        private readonly RulesService _rulesService;
        public RulesController(RulesService rulesService) {
            _rulesService = rulesService;

        }

        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {
            var response = _rulesService.Get();
            return response.Error ? BadRequest(response) : Ok(response);
        }

        [HttpPut(nameof(Put))]
        public async Task<IActionResult> Put(RulesVM rulesVM)
        {

            var response = await _rulesService.Put(rulesVM);
            return response.Error ? BadRequest(response) : Ok(response);
        }
    }
}
