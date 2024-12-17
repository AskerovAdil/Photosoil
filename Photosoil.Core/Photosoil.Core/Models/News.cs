using Photosoil.Core.Models.Second;
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
        public string? CreatedDate { get; set; }

        public List<NewsTranslation> Translations { get; set; } = new();
        public List<Tag> Tags{ get; set; } = new();

        public List<File> ObjectPhoto { get; set; } = new();
        public List<File> Files { get; set; } = new();

        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }

}
