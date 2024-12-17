using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class AuthorResponse
    {
            public int Id { get; set; }
            public AuthorType AuthorType { get; set; }


            public Translation DataEng { get; set; }
            public Translation DataRu { get; set; }
            public string? CreatedDate { get; set; }

            [Display(Name = "Контакты")]
            public string[]? Contacts { get; set; } = new string[] { };

            public string[]? OtherProfiles { get; set; } = new string[] { };

            public int? PhotoId { get; set; }
            public File? Photo { get; set; }

            public int UserId { get; set; }

    }
}
