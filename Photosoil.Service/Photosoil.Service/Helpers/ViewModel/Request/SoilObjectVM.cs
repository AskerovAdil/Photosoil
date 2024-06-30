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
        /// <summary>
        /// Название объекта
        /// </summary>
        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string Name { get; set; }

        /// <summary>
        /// Географическая привязка
        /// </summary>
        public string? GeographicLocation { get; set; }

        /// <summary>
        /// Расположение объекта в рельефе
        /// </summary>
        public string? ReliefLocation { get; set; }

        /// <summary>
        /// Растительное сообщество
        /// </summary>
        public string? PlantCommunity { get; set; }

        /// <summary>
        /// Особенности почвы
        /// </summary>
        public string? SoilFeatures { get; set; }

        /// <summary>
        /// Сопряжённые компоненты почвенного покрова
        /// </summary>
        public string? AssociatedSoilComponents { get; set; }
        public string? Code { get; set; }

        /// <summary>
        /// Общие комментарии
        /// </summary>
        public string? Comments { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public int PhotoId { get; set; }

        public int[]? Authors { get; set; } = { };

        public bool? IsEnglish{ get; set; } = false;

        /// <summary>
        /// Тип объекта базы данных
        /// </summary>
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;

        public int[]? ObjectPhoto { get; set; } = { };
        public int[]? SoilTerms { get; set; } = { };
        public int[]? Publications { get; set; } = { };
        public int[]? EcoSystems { get; set; } = { };


        public void CopySoilObjectFields(SoilObject source)
        {

            source.Latitude= this.Latitude;
            source.Longtitude = this.Longtitude;
            source.Name = this.Name;
            source.GeographicLocation = this.GeographicLocation;
            source.ReliefLocation = this.ReliefLocation;
            source.PlantCommunity = this.PlantCommunity;
            source.SoilFeatures = this.SoilFeatures;
            source.AssociatedSoilComponents = this.AssociatedSoilComponents;
            source.Code = this.Code;
            source.Comments = this.Comments;
            source.ObjectType = this.ObjectType;


            source.LastUpdated = DateTime.Now.ToString(CultureInfo.InvariantCulture);



        }
    }

}