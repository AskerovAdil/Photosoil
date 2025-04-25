using Photosoil.Core.Models.Second;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photosoil.Core.Models
{

    //Переводы
    //Теги - имя на русском, английском, CRUD для тегов
    //Кикнуть обложку
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public long CreatedDate { get; set; }

        public List<NewsTranslation> Translations { get; set; } = new List<NewsTranslation>();
        public List<Tag> Tags{ get; set; } = new List<Tag>();

        public List<File> ObjectPhoto { get; set; } = new List<File>();
        public List<File> Files { get; set; } = new List<File>();

        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}
