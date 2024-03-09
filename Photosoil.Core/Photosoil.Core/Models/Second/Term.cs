using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photosoil.Core.Models.Base;

namespace Photosoil.Core.Models.Second
{
    public class Term : BaseSecond
    {
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
    }
}
