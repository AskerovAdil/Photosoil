using Photosoil.Core.Enum;
using Photosoil.Core.Models;

using System.ComponentModel.DataAnnotations;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public AuthorType AuthorType { get; set; }

        public Translation DataEng { get; set; }
        public Translation DataRu { get; set; }

        [Display(Name = "Контакты")]
        public string[] Contacts { get; set; } = new string[] { };

        public string[] OtherProfiles { get; set; } = new string[] { };

        public int PhotoId { get; set; }

    }



}