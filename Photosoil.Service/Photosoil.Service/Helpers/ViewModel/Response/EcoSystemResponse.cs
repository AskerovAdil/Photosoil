using File = Photosoil.Core.Models.File;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class EcoSystemResponse : Coordinate
    {
        public int Id { get; set; }

        public string? LastUpdated { get; set; }
        public File? Photo { get; set; }

        public List<EcoTranslation> Translations { get; set; } = new();

    }
}