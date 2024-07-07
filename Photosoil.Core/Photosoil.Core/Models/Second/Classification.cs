using Photosoil.Core.Enum;
using Photosoil.Core.Models.Base;

namespace Photosoil.Core.Models.Second
{
    public class Classification : BaseSecond
    {

        public bool IsMulti { get; set; } = true;
        public List<Term> Terms { get; set; } = new();
        public TranslationMode TranslationMode { get; set; } = TranslationMode.Neutral;

    }


}
