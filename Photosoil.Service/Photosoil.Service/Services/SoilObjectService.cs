using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class SoilObjectService : ISoilObjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SoilObjectService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public ServiceResponse<List<BaseData>> GetBaseAll()
        {
            var ecoSystem = _context.SoilObjects.Include(x => x.Translations)
                .ToList();
            List<BaseData> baseData = new();

            foreach (var el in ecoSystem)
            {

                var data = new BaseData(el.Id);
                foreach (var trans in el.Translations)
                {
                    if (trans.IsEnglish == true)
                    {
                        data.CodeEng = trans.Code;
                        data.NameEng = trans.Name;
                    }
                    else
                    {
                        data.CodeRu = trans.Code;
                        data.NameRu = trans.Name;
                    }
                }
                baseData.Add(data);
            }

            return ServiceResponse<List<BaseData>>.OkResponse(baseData);
        }
        public ServiceResponse<List<SoilTranslation>> GetAdminAll(int? userId = 0, string? role = "")
        {
            IQueryable<SoilTranslation> soilObjects;
            if (role == "Moderator")
                soilObjects = _context.SoilTranslations.Include(x => x.SoilObject).ThenInclude(x=>x.User).Where(x => x.SoilObject.UserId == userId);
            else if (role == "Admin")
                soilObjects = _context.SoilTranslations.Include(x => x.SoilObject).ThenInclude(x => x.User);
            else
                soilObjects = _context.SoilTranslations.Include(x => x.SoilObject).ThenInclude(x => x.User);



            var responseSoils = new List<SoilTranslation>();

            return ServiceResponse<List<SoilTranslation>>.OkResponse(soilObjects.ToList());
        }

        public ServiceResponse<List<SoilResponse>> Get(int? userId = 0, string? role = "")
        {

            IQueryable<SoilObject> soilObjects;
            if (role == "Admin" || role == "Moderator")
                soilObjects = _context.SoilObjects.Include(x => x.User).Include(x => x.Authors).Include(x => x.Translations).Include(x => x.Photo).Include(x => x.Terms).AsNoTracking();
            else
                soilObjects = _context.SoilObjects.Include(x=>x.Authors)
                    .Include(x => x.Translations.Where(x => x.IsVisible == true))
                    .Where(x => x.Translations.Count(x => x.IsVisible == true) > 0)
                    .Include(x=>x.User)
                    .Include(x => x.Photo)
                    .Include(x => x.Terms).AsNoTracking();



            var responseSoils = new List<SoilResponse>();
            foreach (var soil in soilObjects)
            {
                var response = _mapper.Map<SoilResponse>(soil);
                responseSoils.Add(response);
            }



            return ServiceResponse<List<SoilResponse>>.OkResponse(responseSoils);
        }

        public ServiceResponse<SoilResponseById>GetById(int id)
        {
            var soilObject =_context.SoilObjects
                .AsNoTracking()
                .Include(x => x.Photo)
                .Include(x=>x.User)
                .Include(x => x.Translations)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Authors).ThenInclude(x => x.Photo)
                .Include(x => x.Authors).ThenInclude(x=>x.DataEng)
                .Include(x => x.Authors).ThenInclude(x=>x.DataRu)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Translations)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Publications).ThenInclude(x => x.Translations)
                .Include(x => x.Terms).ThenInclude(x=>x.Classification)
                .AsNoTracking()
                .FirstOrDefault(x=>x.Id == id);

            if (soilObject == null)
                ServiceResponse<SoilResponseById>.BadResponse(ErrorMessage.NoContent);

            var soilObjectResponse = _mapper.Map<SoilResponseById>(soilObject);

            var classificationVMs = soilObject.Terms
                .GroupBy(t => t.ClassificationId)
                .Select(g => new ClassificationResponse
                {
                    NameRu = g.First().Classification.NameRu,
                    NameEng = g.First().Classification.NameEng,
                    TranslationMode = g.First().Classification.TranslationMode,
                    Terms = g.Select(t=>_mapper.Map<TermsResponse>(t)).ToList()
                })
                .ToList();



            soilObjectResponse.Classification = classificationVMs;

            return soilObject!=null 
                ? ServiceResponse<SoilResponseById>.OkResponse(soilObjectResponse) 
                : ServiceResponse<SoilResponseById>.BadResponse(ErrorMessage.NoContent);
        }

        public ServiceResponse<SoilObjectVM> GetForUpdate(int id)
        {
            var soilObject = _context.SoilObjects
                .AsNoTracking()
                .Include(x => x.Photo)
                .Include(x => x.Translations)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Authors)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x => x.Classification)
                .FirstOrDefault(x => x.Id == id);

            var soilObjectResponse = _mapper.Map<SoilObjectVM>(soilObject);


            return soilObject != null
                ? ServiceResponse<SoilObjectVM>.OkResponse(soilObjectResponse)
                : ServiceResponse<SoilObjectVM>.BadResponse(ErrorMessage.NoContent);
        }




        //Проверить как отработает несозданный Translation создастся ли и замапится ли?
        public async Task<ServiceResponse<SoilObject>> Post(int userId, SoilObjectVM soil)
        {
            try
            {
                var newSoil = _mapper.Map<SoilObject>(soil);
                newSoil.PhotoId = soil.PhotoId;
                newSoil.UserId = userId;

                foreach (var id in soil.ObjectPhoto)
                {
                    var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                    newSoil.ObjectPhoto.Add(q);
                }
                foreach (var id in soil.Authors)
                {
                    var q = _context.Author.FirstOrDefault(x => x.Id == id);
                    newSoil.Authors.Add(q);
                }
                foreach (var id in soil.SoilTerms)
                {
                    var q = _context.Term.FirstOrDefault(x => x.Id == id);
                    newSoil.Terms.Add(q);
                }
                foreach (var id in soil.Publications)
                {
                    var q = _context.Publication.FirstOrDefault(x => x.Id == id);
                    newSoil.Publications.Add(q);
                }
                foreach (var id in soil.EcoSystems)
                {
                    var q = _context.EcoSystem.FirstOrDefault(x => x.Id == id);
                    newSoil.EcoSystems.Add(q);
                }
                soil.Translations.ForEach(x => x.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    
                newSoil.Translations = soil.Translations;
                newSoil.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _context.SoilObjects.Add(newSoil);

                await _context.SaveChangesAsync();



                return ServiceResponse<SoilObject>.OkResponse(newSoil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }


        public async Task<ServiceResponse<SoilObject>> PutVisible(int id, bool isVisible)
        {
            try
            {
                var translation = _context.SoilTranslations.Include(x=>x.SoilObject).FirstOrDefault(x => x.Id == id);
                if(translation == null)
                    return ServiceResponse<SoilObject>.BadResponse("Данные не найдены");

                translation.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                translation.IsVisible = isVisible;

                _context.SoilObjects.Update(translation.SoilObject);
                await _context.SaveChangesAsync();
                return ServiceResponse<SoilObject>.OkResponse(translation.SoilObject);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse<SoilObject>> Put(int id, SoilObjectVM soilObjectVm)
        {
            try
            {
                var soilObject = _context.SoilObjects
                    .Include(x => x.Terms)
                    .Include(x => x.EcoSystems)
                    .Include(x => x.Publications)
                    .Include(x => x.Authors)
                    .Include(x => x.ObjectPhoto).FirstOrDefault(x => x.Id == id);


                _mapper.Map(soilObjectVm, soilObject);

                var photos = soilObjectVm.ObjectPhoto
                    .Select(photoId => _context.Photo.FirstOrDefault(x => x.Id == photoId))
                    .ToList();

                var authors = soilObjectVm.Authors
                    .Select(authorId => _context.Author.FirstOrDefault(x => x.Id == authorId))
                    .ToList();

                var newTerms = soilObjectVm.SoilTerms
                    .Select(idSoilTerm => _context.Term.FirstOrDefault(x => x.Id == idSoilTerm))
                    .ToList();

                var newPublication = soilObjectVm.Publications
                    .Select(publId => _context.Publication.FirstOrDefault(x => x.Id == publId))
                    .ToList();

                var newSystem = soilObjectVm.EcoSystems
                    .Select(ecoId => _context.EcoSystem.FirstOrDefault(x => x.Id == ecoId))
                    .ToList();

                var newTranslations= soilObjectVm.EcoSystems
                    .Select(ecoId => _context.EcoSystem.FirstOrDefault(x => x.Id == ecoId))
                    .ToList();



                //У перевода может быть ID - тогда просто добавляем его в новый массив переводов
                //Может не быть ID - тогда создаем его заново
                foreach(var el in soilObjectVm.Translations)
                {
                    el.SoilId = id;
                    el.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    el.SoilObject = null;
                    if (_context.SoilTranslations.Any(x=>x.Id == el.Id))
                        _context.SoilTranslations.Update(el);
                    else
                        _context.SoilTranslations.Add(el);
                }

                soilObject.ObjectPhoto= new List<File>();
                soilObject.Authors = new List<Author>();
                soilObject.Terms= new List<Term>();
                soilObject.Publications = new List<Publication>();
                soilObject.EcoSystems = new List<EcoSystem>();


                soilObject.EcoSystems.AddRange(newSystem);
                soilObject.Publications.AddRange(newPublication);
                soilObject.EcoSystems.AddRange(newSystem);
                soilObject.Terms.AddRange(newTerms);
                soilObject.Authors.AddRange(authors);
                soilObject.ObjectPhoto.AddRange(photos);

                soilObject.PhotoId = soilObjectVm.PhotoId;


                _context.SoilObjects.Update(soilObject);
                _context.SaveChanges();

                await _context.SaveChangesAsync();
                return ServiceResponse<SoilObject>.OkResponse(soilObject);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }
       
        public async Task<ServiceResponse<SoilObject>> PostMass(SoilMass soil)
        {
            try
            {
                var path = await FileHelper.SavePhoto(soil.Photo.File);
                var photo = new Core.Models.File(path, soil.Photo.TitleEng, soil.Photo.TitleRu);

                var newSoil = _mapper.Map<SoilObject>(soil);
                newSoil.Photo = photo;
                _context.SoilObjects.Add(newSoil);
                await _context.SaveChangesAsync();

                return ServiceResponse<SoilObject>.OkResponse(newSoil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int TranslationId)
        {
            var translation = _context.SoilTranslations
                .Include(x => x.SoilObject).ThenInclude(x => x.Translations)
                .Include(x => x.SoilObject).ThenInclude(x => x.Publications)
                .Include(x => x.SoilObject).ThenInclude(x => x.User)
                .Include(x => x.SoilObject).ThenInclude(x => x.Authors)
                .Include(x => x.SoilObject).ThenInclude(x => x.EcoSystems)
                .Include(x => x.SoilObject).ThenInclude(x => x.ObjectPhoto)
                .Include(x => x.SoilObject).ThenInclude(x => x.Terms)
                .FirstOrDefault(x=>x.Id == TranslationId);

            try
            {
                if (translation != null)
                {
                    if (translation.SoilObject.Translations.Count <= 1)
                    {
                        _context.Remove(translation);
                        _context.SoilObjects.Remove(translation.SoilObject);
                    }
                    else
                        _context.Remove(translation);
                }
                
                _context.SaveChanges();
                return ServiceResponse.OkResponse;
            }
            catch (Exception ex)
            {
                return ServiceResponse.BadResponse(ex.Message);
            }

        }

        public ServiceResponse<List<SoilResponse>> GetByType(SoilObjectType soilType)
        {

            var soilObjects = _context.SoilObjects.Where(x => x.ObjectType == soilType)
                .Include(x => x.Photo)
                .Include(x => x.Terms)
                .ToList();


            var responseSoils = new List<SoilResponse>();
            foreach (var soil in soilObjects)
            {
                var response = _mapper.Map<SoilResponse>(soil);
                responseSoils.Add(response);
            }



            return ServiceResponse<List<SoilResponse>>.OkResponse(responseSoils);


        }

        public ServiceResponse<List<SoilResponse>> GetByFilter(params int[] ids)
        {
            var soils = _context.SoilObjects
                .Include(x => x.Photo)
                .Include(x => x.Terms)
                .ToList();

            var soilObjects =
                soils.Where(soilObject => ids.All(x => soilObject.Terms.Exists(term => term.Id == x))).ToList();

            var responseSoils = new List<SoilResponse>();
            foreach (var soil in soilObjects)
            {
                var response = _mapper.Map<SoilResponse>(soil);
                responseSoils.Add(response);
            }



            return ServiceResponse<List<SoilResponse>>.OkResponse(responseSoils);

        }
    }
}
 