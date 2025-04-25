using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photosoil.Core.Models.Second;

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

        public string Doi { get; set; }

        public PublicationType? Type { get; set; }


        public string Coordinates { get; set; }
        public long CreatedDate { get; set; }

        public int? FileId { get; set; }

        public File File { get; set; }
        
        public List<EcoSystem> EcoSystems { get; set; } = new List<EcoSystem>();
        
        public List<SoilObject> SoilObjects { get; set; } = new List<SoilObject>();
        
        public List<PublicationTranslation> Translations { get; set; } = new List<PublicationTranslation>();
        
        public int? UserId { get; set; }
        
        public ApplicationUser User { get; set; }
    }
    public enum PublicationType
    {
        [Display(Name = "Статья")]
        Article,
        [Display(Name = "Тезисы докладов")]
        Paper,
        [Display(Name = "Монографии")]
        Monographs,
        [Display(Name = "Другое")]
        Other
    }
}


//{
//    type: article(статья) / сonference - paper(тезисы доклада);
//    title: string,
//    authors: string,
//    edition: string,
//    description: string,
//    doi: string,
//    url: string
//}
