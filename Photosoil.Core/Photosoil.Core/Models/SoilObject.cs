using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;

namespace Photosoil.Core.Models
{
    /// <summary>
    /// Модель "Почвенный объект"
    /// </summary>
    public class SoilObject : Coordinate
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PhotoId { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File? Photo{ get; set; }

        public string? Code { get; set; }



        /// <summary>
        /// Тип объекта базы данных
        /// </summary>
        public SoilObjectType? ObjectType { get; set; } = SoilObjectType.SoilDynamics;



        public List<SoilTranslation> Translations{ get; set; } = new();

        public List<Author> Authors { get; set; } = new();
        
        /// <summary>
        /// Фотографии объекта
        /// </summary>
        public List<File> ObjectPhoto { get; set; } = new();

        public List<Term> Terms { get; set; } = new();
        [JsonIgnore]
        public List<EcoSystem> EcoSystems { get; set; } = new();
        [JsonIgnore]
        public List<Publication> Publications { get; set; } = new();

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
