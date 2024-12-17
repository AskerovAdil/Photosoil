using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Photosoil.Core.Models.Base;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Core.Models.Second;
using System.Globalization;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class RulesVM
    {

        public string? ContentRu { get; set; }
        public string? ContentEng { get; set; }

        public int[]? Files { get; set; } = { };

    }

}