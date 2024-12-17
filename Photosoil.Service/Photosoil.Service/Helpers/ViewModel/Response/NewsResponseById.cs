using Photosoil.Core.Models.Second;
using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Photosoil.Core.Models.File;
namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class NewsResponseById
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? CreatedDate { get; set; }

        public List<NewsTranslation> Translations { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();

        public List<File> ObjectPhoto { get; set; } = new();
        public List<File> Files { get; set; } = new();

        public int UserId { get; set; }
        public string? UserEmail { get; set; }
    }
}
