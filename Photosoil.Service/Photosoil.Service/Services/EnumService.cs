using AutoMapper;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Services
{
    public record QuantityObject(int SoilObject, int News, int Publication, int EcoSystem);
    public class EnumService : IEnumService
    {
        private readonly ApplicationDbContext _context;
        public EnumService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<QuantityObject> GetQuantity()
        {
            var SoilObject = _context.SoilObjects.Count();
            var News = _context.News.Count();
            var Publication = _context.Publication.Count();
            var EcoSystem = _context.EcoSystem.Count();

            return new QuantityObject(SoilObject, News, Publication, EcoSystem);
        }

        public Dictionary<int, string> GetPublicationNames()
        {
            var enumDisplayNames = new Dictionary<int, string>();

            var soilObjectTypeValues = Enum.GetValues(typeof(PublicationType));
            foreach (var value in soilObjectTypeValues)
            {
                var displayName = ((PublicationType)value).GetDisplayName();
                enumDisplayNames.Add((int)value, displayName);
            }
            return enumDisplayNames;
        }


        public Dictionary<int, string> GetSoilObjectNames()
        {
            var enumDisplayNames = new Dictionary<int, string>();

            var soilObjectTypeValues = Enum.GetValues(typeof(SoilObjectType));
            foreach (var value in soilObjectTypeValues)
            {
                var displayName = ((SoilObjectType)value).GetDisplayName();
                enumDisplayNames.Add((int)value, displayName);
            }
            return enumDisplayNames;
        }

        public Dictionary<int, string> GetTranslationMode()
        {
            var enumDisplayNames = new Dictionary<int, string>();

            var soilObjectTypeValues = Enum.GetValues(typeof(TranslationMode));
            foreach (var value in soilObjectTypeValues)
            {
                var displayName = ((TranslationMode)value).GetDisplayName();
                enumDisplayNames.Add((int)value, displayName);
            }
            return enumDisplayNames;
        }

        public Dictionary<int, string> GetAuthorType(string lang)
        {
            var enumDisplayNames = new Dictionary<int, string>();

            var soilObjectTypeValues = Enum.GetValues(typeof(AuthorType));
            foreach (var value in soilObjectTypeValues)
            {
                var displayName = ((AuthorType)value).GetDisplayName().Split(";");
                if (lang == "en")
                    enumDisplayNames.Add((int)value, displayName.Last());
                else
                    enumDisplayNames.Add((int)value, displayName.First());
            }
            return enumDisplayNames;
        }
    }
}
