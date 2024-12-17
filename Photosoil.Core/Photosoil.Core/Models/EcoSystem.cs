using Photosoil.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photosoil.Core.Models.Second;

namespace Photosoil.Core.Models
{
    public class EcoSystem : Coordinate
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PhotoId { get; set; }
        
        public bool? IsExternal { get; set; }

        public string? CreatedDate { get; set; }

        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File? Photo { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<Publication> Publications { get; set; } = new();
        public List<Author> Authors { get; set; } = new();
        public List<EcoTranslation> Translations { get; set; } = new();

        /// <summary>
        /// Фотографии объекта
        /// </summary>
        public List<File> ObjectPhoto { get; set; } = new();
        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
