using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Models
{
    public class NewsVM
    {

        public int[]? ObjectPhoto { get; set; } = { };
        public int[]? Files { get; set; } = { };
        public int[]? Tags { get; set; } = { };

        public List<NewsTranslation> Translations { get; set; } = new();
    }
}
