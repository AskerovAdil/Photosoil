using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Base;
using System.Collections.Generic;

namespace Photosoil.Core.Models.Second
{
    public class Classification : BaseSecond
    {
        public bool IsMulti { get; set; } = true;
        public List<Term> Terms { get; set; } = new List<Term>();
        public int Order { get; set; } = 1;
        public TranslationMode TranslationMode { get; set; } = TranslationMode.Neutral;
    }
}