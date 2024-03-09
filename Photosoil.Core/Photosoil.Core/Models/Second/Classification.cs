using Photosoil.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Core.Models.Second
{
    public class Classification : BaseSecond
    {

        public bool IsMulti { get; set; } = true;
        public List<Term> Terms { get; set; } = new();

    }
}
