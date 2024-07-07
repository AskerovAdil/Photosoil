using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Photosoil.Core.Models.Second
{
    public class PublicationTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Edition { get; set; }
        public string? Authors { get; set; }
        public bool? IsVisible { get; set; } = false;
        public bool? IsEnglish { get; set; } = false;
        public string? LastUpdated { get; set; }

        public int? PublicationId { get; set; }

        public Publication? Publication { get; set; }
    }
}
