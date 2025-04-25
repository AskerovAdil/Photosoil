using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Core.Models.Second
{
    public class NewsTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Annotation { get; set; }

        public bool? IsVisible { get; set; } = false;
        public bool? IsEnglish { get; set; } = false;
        public long LastUpdated { get; set; }

        public int? NewsId { get; set; }
        public News? News{ get; set; }

        [NotMapped]
        public User? UserInfo => News is not null && News.User is not null ? new User
        {
            Id = News.User.Id,
            Name = News.User.Name,
            Role = News.User.Role,
            Email = News.User.Email,
        } : null;
    }
}
