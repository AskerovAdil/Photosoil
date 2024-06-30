using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;
using System.ComponentModel.DataAnnotations;
using Photosoil.Core.Models.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class EcoSystemVM : Coordinate
    {
        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string? Name { get; set; }
        public string? Description { get; set; }


        public bool? IsEnglish { get; set; } = false;

        public int[]? Authors { get; set; } = { };
        public int[]? Publications { get; set; } = { };
        public int[]? ObjectPhoto { get; set; } = { };
        public int PhotoId { get; set; }

    }
}