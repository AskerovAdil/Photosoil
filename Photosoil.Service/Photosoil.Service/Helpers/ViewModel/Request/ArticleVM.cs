using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class ArticleVM
    {
        public string Summary { get; set; }
        public string Body { get; set; }
        public PhotoBase Photo { get; set; }

        
    }
}
