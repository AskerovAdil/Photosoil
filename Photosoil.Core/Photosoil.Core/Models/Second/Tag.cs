using Newtonsoft.Json;
using Photosoil.Core.Models.Base;

namespace Photosoil.Core.Models.Second
{
    public class Tag : BaseSecond
    {
        [JsonIgnore]
        public List<News> News { get; set; } = new(); 
    }
}
