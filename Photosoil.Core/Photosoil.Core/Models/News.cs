using System.ComponentModel.DataAnnotations;

namespace Photosoil.Core.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }

    }
}
