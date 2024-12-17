using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Photosoil.Core.Models
{
    public class AuthorRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Organization { get; set; }

        [Display(Name = "Должность")]
        public string? Position { get; set; }

        public string? Email { get; set; }
    }
}
