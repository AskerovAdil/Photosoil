using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public long LastUpdated { get; set; }

        public int? PublicationId { get; set; }

        public Publication? Publication { get; set; }

        [NotMapped]
        public User? UserInfo => Publication is not null && Publication.User is not null ? new User
        {
            Id = Publication.User.Id,
            Name = Publication.User.Name,
            Role = Publication.User.Role,
            Email = Publication.User.Email,
        } : null;
    }
}
