using Photosoil.Core.Enum;
using Photosoil.Service.Abstract;
using Photosoil.Service.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Services
{
    public class EnumService : IEnumService
    {


        public Dictionary<int, string> GetSoilObjectNames()
        {
            var enumDisplayNames = new Dictionary<int, string>();

            var soilObjectTypeValues = Enum.GetValues(typeof(SoilObjectType));
            foreach (var value in soilObjectTypeValues)
            {
                var displayName = ((SoilObjectType)value).GetDisplayName();
                enumDisplayNames.Add((int)value, displayName);
            }
            // Добавьте другие перечисления, если они есть
            return enumDisplayNames;
        }
    }
}
