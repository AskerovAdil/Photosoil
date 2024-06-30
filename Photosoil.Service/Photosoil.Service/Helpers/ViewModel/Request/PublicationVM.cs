using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;
using System.ComponentModel.DataAnnotations;
using Photosoil.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class PublicationVM 
    {

        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Edition { get; set; }
        public string? Authors { get; set; }
        public string? Doi { get; set; }
        
        public PublicationType? Type { get; set; }

        public int? FileId { get; set; }


        //public PhotoBase? File { get; set; }
    }
}


