using Photosoil.Service.Helpers.ViewModel.Base;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class ArticleVM
    {
        public string Summary { get; set; }
        public string Body { get; set; }
        public PhotoBase Photo { get; set; }

        
    }
}
