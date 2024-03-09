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
    public class Publication
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' является обязательным")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FileId { get; set; }

        [Required(ErrorMessage = "Поле 'Изображение' является обязательным")]
        public File? File { get; set; }
        public List<EcoSystem> EcoSystems { get; set; } = new();

        public List<SoilObject> SoilObjects { get; set; } = new();
    }
}
