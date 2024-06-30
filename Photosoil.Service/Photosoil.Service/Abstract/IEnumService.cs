using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Abstract
{
    public interface IEnumService
    {
        Dictionary<int, string> GetSoilObjectNames();
        Dictionary<int, string> GetPublicationNames();

    }
}
