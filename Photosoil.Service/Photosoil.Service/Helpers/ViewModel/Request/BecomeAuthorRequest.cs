using System.ComponentModel.DataAnnotations;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class BecomeAuthorRequest
    {
        [Required(ErrorMessage = "ФИО обязательно для заполнения")]
        public string FullName { get; set; }

        public string Organization { get; set; }

        public string Position { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }
    }
}