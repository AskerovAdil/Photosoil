using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Photosoil.Core.Models
{

    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            Role = "Moderator";
        }

        public string Name { get; set; }

        public string Role { get; set; }

        [Column("RefreshToken")]
        public string RefreshToken { get; set; }

        public List<SoilObject> SoilObjects { get; set; } = new List<SoilObject>();
        public List<EcoSystem> EcoSystems { get; set; } = new List<EcoSystem>();
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<News> News{ get; set; } = new List<News>();
    }

    public enum Roles
    {
        Admin,
        Moderator
    }
}
