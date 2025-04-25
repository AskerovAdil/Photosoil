using Microsoft.AspNetCore.Http;

namespace Photosoil.Service.Helpers.ViewModel.Base
{
    public class PhotoBase
    {
        public string TitleEng { get; set; }
        public string TitleRu { get; set; }
        public IFormFile File { get; set; }
    }
}
