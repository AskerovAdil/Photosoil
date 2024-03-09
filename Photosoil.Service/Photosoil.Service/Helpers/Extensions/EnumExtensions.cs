using Photosoil.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this SoilObjectType value)
        {
            var displayAttribute = value.GetType()
                                        .GetMember(value.ToString())
                                        .First()
                                        .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute != null ? displayAttribute.GetName() : value.ToString();
        }
    }
}

