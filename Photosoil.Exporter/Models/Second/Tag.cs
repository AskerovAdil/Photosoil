using Newtonsoft.Json;
using Photosoil.Core.Models.Base;
using System.Collections.Generic;

namespace Photosoil.Core.Models.Second
{
    public class Tag : BaseSecond
    {
        [JsonIgnore]
        public List<News> News { get; set; } = new List<News>(); 
    }
}
