using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Core.Models.Base
{
    /// <summary>
    /// Координаты
    /// </summary>
    public class Coordinate 
    {
        /// <summary>
        /// Широта десятичные
        /// </summary>
        [Display(Name = "Широта, десятичные координты")]
        public string Latitude{ get; set; }

        /// <summary>
        /// Долгота десятичные
        /// </summary>
        [Display(Name = "Долгота, десятичные координты")]
        public string Longtitude{ get; set; }

    }
}
