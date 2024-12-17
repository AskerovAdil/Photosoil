using Photosoil.Core.Models;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class EcoSystemVM : Coordinate
    {
        public int[]? Authors { get; set; } = { };
        public int[]? SoilObjects { get; set; } = { };
        public int[]? Publications { get; set; } = { };
        public int[]? ObjectPhoto { get; set; } = { };
        public int PhotoId { get; set; }

        public bool? IsExternal { get; set; }
        public string? ExternalSource { get; set; }

        public List<EcoTranslation> Translations { get; set; } = new();
    }
}