using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class ClassificationVM
    {
        public string Name { get; set; }
        public bool IsMulti { get; set; } = true;
        public List<string>? Terms { get; set; } = new();

        public TranslationMode TranslationMode { get; set; } = TranslationMode.Neutral;

    }
}
