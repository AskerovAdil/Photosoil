using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;
using System.ComponentModel.DataAnnotations;
using File = Photosoil.Core.Models.File;
using Photosoil.Core.Models.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class EcoSystemResponse : Coordinate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? LastUpdated { get; set; }
        public bool? IsVisible { get; set; } = false;

        public bool? IsEnglish { get; set; } = false;
        public int? OtherLangId { get; set; }

        public File? Photo { get; set; }
    }
}