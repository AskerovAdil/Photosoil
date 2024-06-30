using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Photosoil.Service.Helpers.ViewModel.Base
{
    public class PhotoBase
    {
        public string? TitleEng { get; set; }
        public string? TitleRu { get; set; }
        public IFormFile? File { get; set; }
    }
}
