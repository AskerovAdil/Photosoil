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

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class SoilObjectResponse : Coordinate
    {
        public int Id { get; set; }

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

        /// <summary>
        /// Общие комментарии
        /// </summary>
        public string? Comments { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public Core.Models.File Photo { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }

        /// <summary>
        /// Тип объекта базы данных
        /// </summary>
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;

        public List<PublicationResponse> Publications { get; set; }
        public List<EcoSystemResponse> EcoSystems { get; set; }
        public List<ClassificationResponse> Classification { get; set; }
    }


}