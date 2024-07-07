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
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class PublicationVM 
    {

        public List<PublicationTranslation> Translations { get; set; } = new();

        public string? Doi { get; set; }
        
        public PublicationType? Type { get; set; }

        public int? FileId { get; set; }


        //public PhotoBase? File { get; set; }
    }
}


