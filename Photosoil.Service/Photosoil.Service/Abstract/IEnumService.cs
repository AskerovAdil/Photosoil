using Photosoil.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Abstract
{
    public interface IEnumService
    {
        Task<QuantityObject> GetQuantity();
        Dictionary<int, string> GetSoilObjectNames();
        Dictionary<int, string> GetPublicationNames();
        Dictionary<int, string> GetTranslationMode();
        Dictionary<int, string> GetAuthorType(string lang);

    }
}
