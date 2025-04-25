using Photosoil.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public long CreatedDate { get; set; }

        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File Photo { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new List<SoilObject>();
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<EcoTranslation> Translations { get; set; } = new List<EcoTranslation>();

        /// <summary>
        /// Фотографии объекта
        /// </summary>
        public List<File> ObjectPhoto { get; set; } = new List<File>();
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
