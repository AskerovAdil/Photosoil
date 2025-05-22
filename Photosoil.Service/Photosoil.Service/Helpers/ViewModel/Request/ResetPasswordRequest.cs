using System.ComponentModel.DataAnnotations;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}