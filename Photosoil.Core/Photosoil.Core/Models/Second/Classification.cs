using Photosoil.Core.Enum;
using Photosoil.Core.Models.Base;

namespace Photosoil.Core.Models.Second
{
    public class Classification : BaseSecond
    {
        public bool IsMulti { get; set; } = true;
        public List<Term> Terms { get; set; } = new();
        public int Order { get; set; } = 1;
        public bool IsAlphabeticallOrder { get; set; } = true;
        public TranslationMode TranslationMode { get; set; } = TranslationMode.Neutral;
    }
}