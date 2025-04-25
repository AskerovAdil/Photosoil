using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Photosoil.Core.Models.Second
{
    public class SoilTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public bool? IsVisible { get; set; } = false;
        public bool? IsEnglish { get; set; } = false;
        public long LastUpdated { get; set; }
        public string? Code { get; set; }
        public string? ExternalSource { get; set; }

        public int? SoilId{ get; set; }
        [JsonIgnore]
        public SoilObject? SoilObject { get; set; }
        
        [NotMapped]
        public User? UserInfo => SoilObject is not null && SoilObject.User is not null ? new User
        {
            Id = SoilObject.User.Id,
            Name = SoilObject.User.Name,
            Role = SoilObject.User.Role,
            Email = SoilObject.User.Email,
        } :null;
    }
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
