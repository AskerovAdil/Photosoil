using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using System.Collections.Generic;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class PublicationVM 
    {

        public List<PublicationTranslation> Translations { get; set; } = new List<PublicationTranslation>();

        public string Doi { get; set; }
        public string Coordinates { get; set; }

        public PublicationType Type { get; set; }

        public int FileId { get; set; }

        public int[] SoilObjects { get; set; } = { };
        public int[] EcoSystems { get; set; } = { };
        //public PhotoBase File { get; set; }
    }
}


