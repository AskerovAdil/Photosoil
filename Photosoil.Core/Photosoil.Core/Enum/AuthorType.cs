using System.ComponentModel.DataAnnotations;

namespace Photosoil.Core.Enum
{
    public enum AuthorType
    {
        [Display(Name = "Главный редактор;Main redactor")]
        MainEditor,
        [Display(Name = "Ответственный редактор;Executive editor")]
        ExecutiveEditor,
        [Display(Name = "Редактор;Editor")]
        Editor, 
        [Display(Name = "Автор;Author   ")]
        Author
    }
}
