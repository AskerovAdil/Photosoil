using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Core.Models.Second;
using Photosoil.Core.Models.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class SoilResponseById : Coordinate
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public long CreatedDate { get; set; }

        public bool? IsExternal { get; set; }


        public string? UserEmail { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File Photo { get; set; }
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;
        public List<AuthorResponse> Authors { get; set; } = new();
        public List<File> ObjectPhoto { get; set; } = new();
        public List<PublicationResponse> Publications { get; set; } = new List<PublicationResponse>();
        public List<EcoSystemResponse> EcoSystems { get; set; } = new List<EcoSystemResponse>();
        public List<ClassificationResponse> Classification { get; set; } = new List<ClassificationResponse>();
        public List<SoilTranslation> Translations { get; set; } = new();


        public object Stats
        {
            get
            {
                return new
                {
                    EcoSystems = new
                    {
                        Ru = EcoSystems?.SelectMany(p => p.Translations)
                                        .Count(t => t.IsEnglish == false) ?? 0,
                        En = EcoSystems?.SelectMany(p => p.Translations)
                                        .Count(t => t.IsEnglish == true) ?? 0
                    },
                    Publications = new
                    {
                        Ru = Publications?.SelectMany(p => p.Translations)
                                          .Count(t => t.IsEnglish == false) ?? 0,
                        En = Publications?.SelectMany(p => p.Translations)
                                          .Count(t => t.IsEnglish == true) ?? 0
                    }
                };
            }
        }


    }
}