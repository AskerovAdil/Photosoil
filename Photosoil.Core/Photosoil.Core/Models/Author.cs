using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Second;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photosoil.Core.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public int? DataEngId { get; set; }
        public Translation? DataEng { get; set; }
        public Translation? DataRu { get; set; }
        public int? DataRuId { get; set; }

        public long CreatedDate { get; set; }

        public AuthorType AuthorType { get; set; }

        [Display(Name = "Профили")]
        public string? OtherProfiles { get; set; }
        [Display(Name = "Контакты")]
        public string? Contacts { get; set; }

        public int? PhotoId { get; set; }
        public File? Photo { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<EcoSystem> EcoSystems{ get; set; } = new();

        public int? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        [NotMapped]
        public User? UserInfo => User is not null ? new User
        {
            Id = User.Id,
            Name = User.Name,
            Role = User.Role,
            Email = User.Email,
        } : null;
    }

    public class Translation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Organization { get; set; }

        public string? Specialization { get; set; }
        [Display(Name = "Ученая степень")]
        public string? Degree { get; set; }

        [Display(Name = "Должность")]
        public string? Position { get; set; }

        public string? Description { get; set; }

    }
}
