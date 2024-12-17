using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photosoil.Core.Models.Second
{
    public class EcoTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Название объекта
        /// </summary>
        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsVisible { get; set; } = false;
        public bool? IsEnglish { get; set; } = false;
        public string? LastUpdated { get; set; }
        public string? Comments { get; set; }

        public string? Code { get; set; }
        public string? ExternalSource { get; set; }

        public int? EcoSystemId { get; set; }
        public EcoSystem? EcoSystem { get; set; }

    }
}
