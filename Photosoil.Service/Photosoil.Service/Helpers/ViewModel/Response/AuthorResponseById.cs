using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class AuthorResponseById
    {
            public int Id { get; set; }

            public AuthorType AuthorType { get; set; }

            public Translation DataEng { get; set; }
            public Translation DataRu { get; set; }
            public long CreatedDate { get; set; }

            [Display(Name = "Контакты")]
            public string[]? Contacts { get; set; } = new string[] { };

            public string[]? OtherProfiles { get; set; } = new string[] { };

            public int? PhotoId { get; set; }
            public File? Photo { get; set; }
            public List<SoilResponse> SoilObjects { get; set; } = new();
            public List<EcoSystemResponse> EcoSystems { get; set; } = new();


        public object Stats
        {
            get
            {
                return new
                {
                    EcoSystems = new
                    {
                        Ru = EcoSystems?.SelectMany(p => p.Translations)
                                        .Where(x=>x.IsVisible == true)
                                        .Count(t => t.IsEnglish == false) ?? 0,
                        En = EcoSystems?.SelectMany(p => p.Translations).Where(x => x.IsVisible == true)
                                        .Count(t => t.IsEnglish == true) ?? 0
                    },
                    SoilObjects = new
                    {
                        Ru = SoilObjects?.SelectMany(p => p.Translations)
                                          .Where(x => x.IsVisible == true)
                                          .Count(t => t.IsEnglish == false) ?? 0,
                        En = SoilObjects?.SelectMany(p => p.Translations).Where(x => x.IsVisible == true)
                                          .Count(t => t.IsEnglish == true) ?? 0
                    }
                };
            }
        }
    }
}
