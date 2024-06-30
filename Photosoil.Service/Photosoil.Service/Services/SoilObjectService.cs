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
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel;
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
        public  ServiceResponse<List<SoilResponse>> Get(string lang = "", int? userId = 0, string? role = "")
        {

            IQueryable<SoilObject> soilObjects;
            if(role == "Moderator")
                soilObjects = _context.SoilObjects.Include(x => x.Photo).Include(x => x.Terms)
                    .Where(x => x.UserId == userId);
            else if (role == "Admin")
               soilObjects = _context.SoilObjects.Include(x => x.Photo).Include(x => x.Terms);
            else
                soilObjects = _context.SoilObjects.Include(x => x.Photo).Include(x => x.Terms).Where(x => x.IsVisible == true);

            if (lang == "en")
                soilObjects = soilObjects.Where(x => x.IsEnglish == true);
            else if(lang == "ru")
                soilObjects = soilObjects.Where(x => x.IsEnglish == false);


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
                .Include(x=>x.Photo)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Authors)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x=>x.Classification)
                .FirstOrDefault(x=>x.Id == id);

            var soilObjectResponse = _mapper.Map<SoilResponseById>(soilObject);

            var classificationVMs = soilObject.Terms
                .GroupBy(t => t.ClassificationId)
                .Select(g => new ClassificationResponse
                {
                    Name = g.First().Classification.Name,
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

        //public async Task<ServiceResponse<SoilObject>> Post(SoilObjectVM soil)
        //{
        //    try
        //    {
        //
        //        var newSoil = _mapper.Map<SoilObject>(soil);
        //        newSoil.LastUpdated = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        //        newSoil.PhotoId = soil.PhotoId;
        //
        //        foreach (var id in soil.ObjectPhoto)
        //        {
        //            var q = _context.Photo.FirstOrDefault(x => x.Id == id);
        //            newSoil.ObjectPhoto.Add(q);
        //        }
        //        foreach (var id in soil.Authors)
        //        {
        //            var q = _context.Author.FirstOrDefault(x => x.Id == id);
        //            newSoil.Authors.Add(q);
        //        }
        //        foreach (var id in soil.SoilTerms)
        //        {
        //            var q = _context.Term.FirstOrDefault(x => x.Id == id);
        //            newSoil.Terms.Add(q);
        //        }
        //        foreach (var id in soil.Publications)
        //        {
        //            var q = _context.Publication.FirstOrDefault(x => x.Id == id);
        //            newSoil.Publications.Add(q);
        //        }
        //        foreach (var id in soil.EcoSystems)
        //        {
        //            var q = _context.EcoSystem.FirstOrDefault(x => x.Id == id);
        //            newSoil.EcoSystems.Add(q);
        //        }
        //        _context.SoilObjects.Add(newSoil);
        //        await _context.SaveChangesAsync();
        //        
        //        return ServiceResponse<SoilObject>.OkResponse(newSoil);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServiceResponse<SoilObject>.BadResponse(ex.Message);
        //    }
        //}

        public async Task<ServiceResponse<List<SoilObject>>> Post(int userId, List<SoilObjectVM> soils)
        {
            try
            {
                var result = new List<SoilObject>();
                for(int i =0; i< soils.Count; i++)
                {
                    var newSoil = _mapper.Map<SoilObject>(soils[i]);
                    newSoil.LastUpdated = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    newSoil.PhotoId = soils[i].PhotoId;
                    newSoil.UserId = userId;

                    foreach (var id in soils[i].ObjectPhoto)
                    {
                        var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                        newSoil.ObjectPhoto.Add(q);
                    }
                    foreach (var id in soils[i].Authors)
                    {
                        var q = _context.Author.FirstOrDefault(x => x.Id == id);
                        newSoil.Authors.Add(q);
                    }
                    foreach (var id in soils[i].SoilTerms)
                    {
                        var q = _context.Term.FirstOrDefault(x => x.Id == id);
                        newSoil.Terms.Add(q);
                    }
                    foreach (var id in soils[i].Publications)
                    {
                        var q = _context.Publication.FirstOrDefault(x => x.Id == id);
                        newSoil.Publications.Add(q);
                    }
                    foreach (var id in soils[i].EcoSystems)
                    {
                        var q = _context.EcoSystem.FirstOrDefault(x => x.Id == id);
                        newSoil.EcoSystems.Add(q);
                    }
                    _context.SoilObjects.Add(newSoil);
                    
                    await _context.SaveChangesAsync();
                    result.Add(newSoil);
                }

                if(result.Count > 1)
                    await MergeLang(result);

                return ServiceResponse<List<SoilObject>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<SoilObject>>.BadResponse(ex.Message);
            }
        }

        private async Task<List<SoilObject>> MergeLang(List<SoilObject> soilObjects)
        {
            var soilRu = soilObjects.FirstOrDefault(); 
            var soilEng = soilObjects.LastOrDefault();

            soilRu.OtherLangId = soilEng.Id;
            soilEng.OtherLangId = soilRu.Id;

            _context.Update(soilRu);
            _context.Update(soilEng);

            _context.SaveChanges();

            return soilObjects;
        }

        public async Task<ServiceResponse<SoilObject>> PutVisible(int id, bool isVisible)
        {
            try
            {
                var soilObject = _context.SoilObjects.FirstOrDefault(x => x.Id == id);
                if(soilObject == null)
                    return ServiceResponse<SoilObject>.BadResponse("Данные не найдены");

                soilObject.LastUpdated = DateTime.Now.ToString();
                soilObject.IsVisible = isVisible;

                _context.SoilObjects.Update(soilObject);
                await _context.SaveChangesAsync();
                return ServiceResponse<SoilObject>.OkResponse(soilObject);
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
                    .Include(x => x.ObjectPhoto).FirstOrDefault(x=>x.Id == id);

                soilObject.ObjectPhoto = new List<File>();
                soilObject.Authors = new List<Author>();
                soilObject.Publications = new List<Publication>();
                soilObject.EcoSystems = new List<EcoSystem>();
                soilObject.Terms = new List<Term>();
                soilObject.PhotoId = soilObjectVm.PhotoId;

                _context.SoilObjects.Update(soilObject);
                _context.SaveChanges();

                soilObjectVm.CopySoilObjectFields(soilObject);


                var photos = soilObjectVm.ObjectPhoto
                    .Select(photoId => _context.Photo.FirstOrDefault(x => x.Id == photoId))
                    .ToList();
                soilObject.ObjectPhoto.AddRange(photos);


                
                var authors = soilObjectVm.Authors
                    .Select(authorId => _context.Author.FirstOrDefault(x => x.Id == authorId))
                    .ToList();
                
                soilObject.Authors.AddRange(authors);
                
                var newTerms = soilObjectVm.SoilTerms
                    .Select(idSoilTerm => _context.Term.FirstOrDefault(x => x.Id == idSoilTerm))
                    .ToList();
                
                soilObject.Terms.AddRange(newTerms);
                
                
                var newPublication= soilObjectVm.Publications
                    .Select(publId=> _context.Publication.FirstOrDefault(x => x.Id == publId))
                    .ToList();
                
                soilObject.Publications.AddRange(newPublication);
                
                var newSystem= soilObjectVm.EcoSystems
                    .Select(ecoId => _context.EcoSystem.FirstOrDefault(x => x.Id == ecoId))
                    .ToList();
                
                soilObject.EcoSystems.AddRange(newSystem);

           

                _context.SoilObjects.Update(soilObject);
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

        public ServiceResponse Delete(int id)
        {
            var soilObject = _context.SoilObjects.AsNoTracking()
                .Include(x=>x.ObjectPhoto)
                .Include(x=>x.Photo).FirstOrDefault(x => x.Id == id);

            try
            {
                if (soilObject != null) _context.Remove(soilObject);
                _context.SaveChanges();
                return ServiceResponse.OkResponse;
            }
            catch (Exception ex)
            {
                return ServiceResponse.BadResponse(ex.Message);
            }

        }
    }
}
 