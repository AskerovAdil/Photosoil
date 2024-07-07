using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class EcoSystemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EcoSystemService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public ServiceResponse<List<EcoTranslation>> GetAdminAll(int? userId = 0, string? role = "")
        {
            IQueryable<EcoTranslation> soilObjects;
            if (role == "Moderator")
                soilObjects = _context.EcoTranslations.Include(x => x.EcoSystem).ThenInclude(x => x.User).Where(x => x.EcoSystem.UserId == userId).AsNoTracking();
            else //role == admin
                soilObjects = _context.EcoTranslations.Include(x => x.EcoSystem).ThenInclude(x => x.User).AsNoTracking();

            return ServiceResponse<List<EcoTranslation>>.OkResponse(soilObjects.ToList());
        }

        public ServiceResponse<List<EcoSystem>> GetAll(int? userId = 0, string? role = "")
        {

            IQueryable<EcoSystem> ecoSystems;
            if (role == "Moderator")
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects).Where(x => x.UserId == userId).AsNoTracking();
            else if (role == "Admin")
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects).AsNoTracking();
            else
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects)
                    .Include(x=>x.Translations.Where(x => x.IsVisible == true)).AsNoTracking();



            return ServiceResponse<List<EcoSystem>>.OkResponse(ecoSystems.ToList());
        }

        public ServiceResponse<EcoSystem> GetById(int id)
        {
            var ecoSystem = _context.EcoSystem
                    .Include(x => x.Translations)
                    .Include(x => x.Photo)
                    .Include(x => x.Publications).ThenInclude(x=>x.Translations)
                    .Include(x => x.ObjectPhoto)
                    .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                    .Include(x => x.SoilObjects).ThenInclude(x => x.Translations)
                    .Include(x => x.Authors).ThenInclude(x=>x.Photo)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id)
                ;
            return ServiceResponse<EcoSystem>.OkResponse(ecoSystem);
        }
        public ServiceResponse<EcoSystemVM> GetForUpdate(int id)
        {
            var item = _context.EcoSystem
                .AsNoTracking()
                .Include(x => x.Photo)
                .Include(x => x.Authors)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Translations)
                .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .FirstOrDefault(x => x.Id == id);

            var itemResponse = _mapper.Map<EcoSystemVM>(item);


            return item != null
                ? ServiceResponse<EcoSystemVM>.OkResponse(itemResponse)
                : ServiceResponse<EcoSystemVM>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<EcoSystem>> Post(int userId, EcoSystemVM ecoSystemVM)
        {
            try
            {
                var ecoSystem = _mapper.Map<EcoSystem>(ecoSystemVM);
                ecoSystem.PhotoId = ecoSystemVM.PhotoId;
                ecoSystem.UserId = userId;

                ecoSystemVM.Translations.ForEach(x => x.LastUpdated = DateTime.Now.ToString());
                foreach (var id in ecoSystemVM.Publications)
                {
                    var publication = _context.Publication.FirstOrDefault(x => x.Id == id);
                    ecoSystem.Publications.Add(publication);
                }

                foreach (var id in ecoSystemVM.Authors)
                {
                    var q = _context.Author.FirstOrDefault(x => x.Id == id);
                    ecoSystem.Authors.Add(q);
                }
                foreach (var id in ecoSystemVM.ObjectPhoto)
                {
                    var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                    ecoSystem.ObjectPhoto.Add(q);
                }

                ecoSystem.Translations = ecoSystemVM.Translations;

                _context.EcoSystem.Add(ecoSystem);
                await _context.SaveChangesAsync();


                return ServiceResponse<EcoSystem>.OkResponse(ecoSystem);
            }
            catch (Exception ex)
            {
                return ServiceResponse<EcoSystem>.BadResponse(ex.Message);
            }
        }


        public async Task<ServiceResponse<EcoSystem>> PutVisible(int id, bool isVisible)
        {
            try
            {

                var eco = await _context.EcoTranslations.Include(x=>x.EcoSystem).FirstOrDefaultAsync(x => x.Id == id);

                if (eco == null)
                    return ServiceResponse<EcoSystem>.BadResponse("Экосистема не найдена!");

                eco.LastUpdated = DateTime.Now.ToString();
                eco.IsVisible = isVisible;

                _context.EcoTranslations.Update(eco);
                _context.SaveChanges();
                return ServiceResponse<EcoSystem>.OkResponse(eco.EcoSystem);
            }
            catch (Exception ex)
            {
                return ServiceResponse<EcoSystem>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<EcoSystem>> Put(int id,EcoSystemVM ecoSystemVM)
        {
            try
            {   
                
                var eco = await _context.EcoSystem.Include(x=>x.Photo).Include(x=>x.ObjectPhoto).Include(x=>x.Publications).Include(x => x.Authors).FirstOrDefaultAsync(x => x.Id == id);
                
                if (eco == null)
                     return ServiceResponse<EcoSystem>.BadResponse("Экосистема не найдена!");

                _mapper.Map(ecoSystemVM,eco);

                var authors = ecoSystemVM.Authors
                     .Select(id => _context.Author.FirstOrDefault(x => x.Id == id))
                     .ToList();
                var publications = ecoSystemVM.Publications
                     .Select(id => _context.Publication.FirstOrDefault(x => x.Id == id))
                     .ToList();
                var photo= ecoSystemVM.ObjectPhoto
                     .Select(id => _context.Photo.FirstOrDefault(x => x.Id == id))
                     .ToList();


                foreach (var el in ecoSystemVM.Translations)
                {
                    el.EcoSystemId = id;
                    el.LastUpdated = DateTime.Now.ToString();
                    el.EcoSystem = null;

                    if (_context.EcoTranslations.Any(x => x.Id == el.Id))
                        _context.EcoTranslations.Update(el);
                    else
                        _context.EcoTranslations.Add(el);
                }

                eco.Authors = new List<Author>();
                eco.ObjectPhoto = new List<File>();
                eco.Publications = new List<Publication>();

                eco.Authors.AddRange(authors);
                eco.ObjectPhoto.AddRange(photo);
                eco.Publications.AddRange(publications);

                _context.EcoSystem.Update(eco);
                 _context.SaveChanges();
                return ServiceResponse<EcoSystem>.OkResponse(eco);
            }
            catch (Exception ex)
            {
                return ServiceResponse<EcoSystem>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int TranslationId)
        {
            var translation = _context.EcoTranslations
                .Include(x => x.EcoSystem).ThenInclude(x => x.Translations).FirstOrDefault(x => x.Id == TranslationId);

            try
            {
                if (translation != null)
                {
                    if (translation.EcoSystem.Translations.Count <= 1)
                    {
                        _context.Remove(translation);
                        _context.Remove(translation.EcoSystem);
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
