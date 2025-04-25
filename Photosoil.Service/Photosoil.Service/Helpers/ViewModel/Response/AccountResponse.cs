using Photosoil.Core.Models;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Role { get; set; }
        public string? Email { get; set; }

        public List<SoilObject> SoilObjects { get; set; } = new();
        public List<EcoSystem> EcoSystems { get; set; } = new();
        public List<Publication> Publications { get; set; } = new();
        public List<Author> Authors { get; set; } = new();
        public List<News> News { get; set; } = new();

    }
}
