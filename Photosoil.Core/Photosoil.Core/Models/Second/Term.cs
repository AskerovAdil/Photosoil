using Photosoil.Core.Models.Base;

namespace Photosoil.Core.Models.Second
{
    public class Term : BaseSecond
    {
        public int Order { get; set; } = 1;
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
    }
}
