using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Core.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public Translation DataEng { get; set; }
        public Translation DataRu { get; set; }

        [Display(Name = "Профили")]
        public string? OtherProfiles { get; set; }
        [Display(Name = "Контакты")]
        public string? Contacts { get; set; }

        public int? PhotoId { get; set; }
        public File? Photo { get; set; }
        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<EcoSystem> EcoSystems{ get; set; } = new();

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }

    public class Translation
    {
        [Key]
        [JsonIgnore]
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
