using File = Photosoil.Core.Models.File;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;
using Photosoil.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Photosoil.Service.Helpers.ViewModel.Response;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class EcoSystemResponse : Coordinate
    {
        public int Id { get; set; }

        public long CreatedDate { get; set; }
        public File? Photo { get; set; }

        public bool? IsExternal { get; set; }

        public List<EcoTranslation> Translations { get; set; } = new();

    }

    public class EcoSystemResponseAll : Coordinate
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PhotoId { get; set; }
        public bool? IsExternal { get; set; }
        public long CreatedDate { get; set; }

        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File? Photo { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<Publication> Publications { get; set; } = new();
        public int[]? Authors { get; set; } = { };

        public List<EcoTranslation> Translations { get; set; } = new();

        /// <summary>
        /// Фотографии объекта
        /// </summary>
        public List<File> ObjectPhoto { get; set; } = new();
        public int? UserId { get; set; }
        public AccountResponse? User { get; set; }

    }
}