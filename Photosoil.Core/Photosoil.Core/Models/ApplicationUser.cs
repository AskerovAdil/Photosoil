using Microsoft.AspNetCore.Identity;
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

        public string? Name { get; set; }

        public string Role { get; set; }

        [Column("RefreshToken")]
        public string? RefreshToken { get; set; }

        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<EcoSystem> EcoSystems { get; set; } = new();
        public List<Publication> Publications { get; set; } = new();
        public List<Author> Authors { get; set; } = new();
    }

    public enum Roles
    {
        Admin,
        Moderator
    }
}
