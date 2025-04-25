using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Photosoil.Service.Helpers.ViewModel.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class PhotoVM
    {
        public string TitleEng { get; set; }
        public string TitleRu { get; set; }
        public IFormFile Photo { get; set; }
    }
}
