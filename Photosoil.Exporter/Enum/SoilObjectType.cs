using System.ComponentModel.DataAnnotations;

namespace Photosoil.Core.Enum
{
    public enum SoilObjectType
    {
        [Display(Name = "Динамика почв")]
        SoilDynamics, //4
        [Display(Name = "Почвенные профили")]
        SoilProfiles,//2
        [Display(Name = "Почвенные морфологические элементы")]
        SoilMorphologicalElements //3
    }
}
