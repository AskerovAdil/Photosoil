using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers.ViewModel.Base;
using System.ComponentModel.DataAnnotations;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class PublicationResponse
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string Name { get; set; }
        public string Description { get; set; }
        public File? File { get; set; }

    }
}