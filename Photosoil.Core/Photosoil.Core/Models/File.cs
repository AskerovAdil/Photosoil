using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Photosoil.Core.Models
{
    public class File
    {
        public File(){}

        public File(string path, string titleEng, string titleRu)
        {
            Path = path;
            TitleEng = titleEng;
            TitleRu= titleRu;
            LastUpdated = DateTime.Now.ToString();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Path { get; set; }
        public string? TitleEng { get; set; }
        public string? TitleRu { get; set; }
        public string? LastUpdated { get; set; }

        [JsonIgnore]
        public List<SoilObject> SoilObjects { get; set; } = new();

        [JsonIgnore]
        public List<EcoSystem> EcoSystems { get; set; } = new();

        [NotMapped]
        public string FileName
        {
            get
            {
                return string.IsNullOrEmpty(Path) ? "Без_названия" : Path.Split('/').Last();
            }
        }

    }
}
