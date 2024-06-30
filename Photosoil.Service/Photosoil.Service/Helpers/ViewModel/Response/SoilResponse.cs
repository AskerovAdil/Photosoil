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
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class SoilResponse : Coordinate
    {
        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string Name { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File Photo { get; set; }
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;


        public string? LastUpdated { get; set; }
        public bool? IsVisible { get; set; } = false;

        public bool? IsEnglish { get; set; } = false;

        public int? OtherLangId { get; set; }

        /// <summary>
        /// Тип объекта базы данных
        /// </summary>
        public int[] Terms { get; set; } = new int[] { };
    }


}