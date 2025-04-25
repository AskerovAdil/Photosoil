using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Photosoil.Core.Enum
{
    public enum TranslationMode
    {
        [Display(Name = "Нейтрально по отношению к языку")]
        Neutral,
        [Display(Name = "Только для английской версии")]
        OnlyEng,
        [Display(Name = "Только для русской версии")]
        OnlyRu
    }
}
