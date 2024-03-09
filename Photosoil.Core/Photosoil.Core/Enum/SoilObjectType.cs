using System.ComponentModel.DataAnnotations;

namespace Photosoil.Core.Enum
{
    public enum SoilObjectType
    {
        [Display(Name = "Динамика почв")]
        SoilDynamics,
        [Display(Name = "Почвенные профили")]
        SoilProfiles,
        [Display(Name = "Почвенные морфологические элементы")]
        SoilMorphologicalElements
    }
}
