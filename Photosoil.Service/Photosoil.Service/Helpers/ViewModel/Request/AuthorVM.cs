using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class AuthorVM
    {
        public string FIO { get; set; }
        public string Description { get; set; }
        public PhotoBase? Photo { get; set; }
    }
}