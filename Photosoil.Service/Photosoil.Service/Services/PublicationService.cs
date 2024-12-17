using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class PublicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PublicationService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public ServiceResponse<List<BaseData>> GetBaseAll()
        {
            var ecoSystem = _context.Publication.Include(x => x.Translations)
                .ToList();
            List<BaseData> baseData = new();

            foreach (var el in ecoSystem)
            {

                var data = new BaseData(el.Id);
                foreach (var trans in el.Translations)
                {
                    if (trans.IsEnglish == true)
                    {
                        data.NameEng = trans.Name;
                    }
                    else
                    {
                        data.NameRu = trans.Name;
                    }
                }
                baseData.Add(data);

            }

            return ServiceResponse<List<BaseData>>.OkResponse(baseData);
        }
        public ServiceResponse<List<PublicationTranslation>> GetAdminAll(int? userId = 0, string? role = "")
        {

            IQueryable<PublicationTranslation> soilObjects;
            if (role == "Moderator")
                soilObjects = _context.PublicationTranslations.Include(x => x.Publication).ThenInclude(x => x.User).Where(x => x.Publication.UserId == userId);
            else //role == admin
                soilObjects = _context.PublicationTranslations.Include(x => x.Publication).ThenInclude(x => x.User);

            return ServiceResponse<List<PublicationTranslation>>.OkResponse(soilObjects.ToList());
        }


        public  ServiceResponse<List<Publication>> GetAll(int? userId = 0, string? role = "")
        {
            var datArticles = new List<Publication>();
            if (role == "Admin" || role == "Moderator" )
                datArticles = _context.Publication.Include(x=>x.Translations).Include(x => x.File).Include(x => x.EcoSystems).Include(x => x.SoilObjects)
                    .AsNoTracking().ToList();
            else
                datArticles = _context.Publication.Include(x => x.Translations.Where(x => x.IsVisible == true))
                    .Where(x => x.Translations.Count(x => x.IsVisible == true) > 0)
                    .Include(x => x.File).Include(x => x.EcoSystems).Include(x => x.SoilObjects)
                    .AsNoTracking().ToList();


            return ServiceResponse<List<Publication>>.OkResponse(datArticles);
        }

        public ServiceResponse<PublicationResponseById> GetById(int articleId)
        {
            var article = _context.Publication.Include(x => x.File)
                .Include(x => x.User)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Translations)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Translations)
                .Include(x => x.SoilObjects).ThenInclude(x => x.Translations)
                .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                .AsNoTracking()
                .FirstOrDefault(x=>x.Id == articleId);

            var result = _mapper.Map<PublicationResponseById>(article); 

            return ServiceResponse<PublicationResponseById>.OkResponse(result);
        }

        public ServiceResponse<PublicationVM> GetForUpdate(int id)
        {
            var soilObject = _context.Publication
                .AsNoTracking()
                .Include(x => x.Translations)
                .Include(x => x.SoilObjects)
                .Include(x => x.EcoSystems)
                .FirstOrDefault(x => x.Id == id);

            var soilObjectResponse = _mapper.Map<PublicationVM>(soilObject);


            return soilObject != null
                ? ServiceResponse<PublicationVM>.OkResponse(soilObjectResponse)
                : ServiceResponse<PublicationVM>.BadResponse(ErrorMessage.NoContent);
        }


        public async Task<ServiceResponse<Publication>> Post(int userId, PublicationVM publicationVM)
        {
            try
            {

                var publication = _mapper.Map<Publication>(publicationVM);
                publication.FileId = publicationVM.FileId;
                publication.UserId = userId;

                publicationVM.Translations.ForEach(x => x.LastUpdated = DateTime.Now.ToString());
                publication.Translations = publicationVM.Translations;
                publication.CreatedDate = DateTime.Now.ToString();
                foreach (var id in publicationVM.SoilObjects)
                {
                    var q = _context.SoilObjects.FirstOrDefault(x => x.Id == id);
                    publication.SoilObjects.Add(q);
                }
                foreach (var id in publicationVM.EcoSystems)
                {
                    var q = _context.EcoSystem.FirstOrDefault(x => x.Id == id);
                    publication.EcoSystems.Add(q);
                }

                _context.Publication.Add(publication);
                await _context.SaveChangesAsync();

                return ServiceResponse<Publication>.OkResponse(publication);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Publication>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Publication>> PutVisible(int id, bool isVisible)
        {
            try
            {
                var trans = await _context.PublicationTranslations
                    .Include(x=>x.Publication)
                    .FirstOrDefaultAsync(x => x.Id == id);

                trans.LastUpdated = DateTime.Now.ToString();
                trans.IsVisible = isVisible;

                if (trans == null)
                    return ServiceResponse<Publication>.BadResponse("Публикация не найдена!");

                _context.PublicationTranslations.Update(trans);
                _context.SaveChanges();
                return ServiceResponse<Publication>.OkResponse(trans.Publication);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Publication>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse<Publication>> Put(int id, PublicationVM publicationVm)
        {

            try
            {
                var publication = await _context.Publication.Include(x=>x.File)
                    .Include(x=>x.EcoSystems)
                    .Include(x=>x.SoilObjects)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                publication.Translations.ForEach(x => x.LastUpdated = DateTime.Now.ToString());
                
                publication.FileId = publicationVm.FileId;

                foreach (var el in publicationVm.Translations)
                {
                    el.PublicationId = id;
                    el.LastUpdated = DateTime.Now.ToString();
                    el.Publication = null;

                    if (_context.PublicationTranslations.Any(x => x.Id == el.Id))
                        _context.PublicationTranslations.Update(el);
                    else
                        _context.PublicationTranslations.Add(el);
                }


                if (publication == null)
                    return ServiceResponse<Publication>.BadResponse("Публикация не найдена!");

                _mapper.Map(publicationVm, publication);



                var newSoils = publicationVm.SoilObjects
                    .Select(soilId => _context.SoilObjects.FirstOrDefault(x => x.Id == soilId))
                    .ToList();

                var newSystem = publicationVm.EcoSystems
                    .Select(ecoId => _context.EcoSystem.FirstOrDefault(x => x.Id == ecoId))
                    .ToList();

                publication.SoilObjects = new List<SoilObject>();
                publication.EcoSystems = new List<EcoSystem>();

                publication.SoilObjects.AddRange(newSoils);
                publication.EcoSystems.AddRange(newSystem);

                _context.Publication.Update(publication);
                _context.SaveChanges();
                return ServiceResponse<Publication>.OkResponse(publication);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Publication>.BadResponse(ex.Message);
            }   
        }


        public ServiceResponse Delete(int TranslationId)
        {
            var translation = _context.PublicationTranslations
                .Include(x => x.Publication).ThenInclude(x => x.Translations).FirstOrDefault(x => x.Id == TranslationId);

            try
            {
                if (translation != null)
                {
                    if (translation.Publication.Translations.Count <= 1)
                    {
                        _context.Remove(translation);
                        _context.Publication.Remove(translation.Publication);
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

    }
}
