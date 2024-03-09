using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Core.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Description { get; set; }
        public int? PhotoId { get; set; }
        public File? Photo { get; set; }
    }
}
