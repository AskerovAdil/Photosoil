using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;
using System.ComponentModel.DataAnnotations;
using File = Photosoil.Core.Models.File;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Helpers.ViewModel.Request
{


    public class PublicationResponse  
    {
        public int Id { get; set; }
        public List<PublicationTranslation> Translations { get; set; } = new();

        public string? Doi { get; set; }

        public string? LastUpdated { get; set; }

        public PublicationType? Type { get; set; }

        public File? File { get; set; }

    }
}