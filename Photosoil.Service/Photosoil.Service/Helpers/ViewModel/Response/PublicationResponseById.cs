using Photosoil.Core.Models;
using File = Photosoil.Core.Models.File;
using Photosoil.Core.Models.Base;
using Photosoil.Service.Helpers.ViewModel.Response;
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class    PublicationResponseById
    {
        public int Id { get; set; }
        public string? Doi { get; set; }
        public long CreatedDate { get; set; }

        public string? UserEmail { get; set; }

        public string? Coordinates { get; set; } 
        public PublicationType? Type { get; set; }

        public File? File { get; set; }
        public List<EcoSystemResponse> EcoSystems { get; set; } = new();
        public List<SoilResponse> SoilObjects { get; set; } = new();
        public List<PublicationTranslation> Translations { get; set; } = new();

    }
}