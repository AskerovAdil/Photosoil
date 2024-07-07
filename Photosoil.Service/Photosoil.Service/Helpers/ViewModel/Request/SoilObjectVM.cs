using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Photosoil.Core.Models.Base;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Core.Models.Second;
using System.Globalization;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class SoilObjectVM : Coordinate
    {

        public string? Code { get; set; }


        public List<SoilTranslation> Translations { get; set; } = new();

        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public int PhotoId { get; set; }

        public int[]? Authors { get; set; } = { };

        /// <summary>
        /// Тип объекта базы данных
        /// </summary>
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;

        public int[]? ObjectPhoto { get; set; } = { };
        public int[]? SoilTerms { get; set; } = { };
        public int[]? Publications { get; set; } = { };
        public int[]? EcoSystems { get; set; } = { };
    }

}