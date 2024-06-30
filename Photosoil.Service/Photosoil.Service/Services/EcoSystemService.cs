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

        public  ServiceResponse<List<EcoSystem>> GetAll(string lang ="", int? userId = 0, string? role = "")
        {

            IQueryable<EcoSystem> ecoSystems;
            if (role == "Moderator")
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects).Where(x => x.UserId == userId);
            else if (role == "Admin")
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects);
            else
                ecoSystems = _context.EcoSystem.Include(x => x.Photo).Include(x => x.Publications).Include(x => x.SoilObjects).Where(x => x.IsVisible == true);



            if (lang == "en")
                ecoSystems = ecoSystems.Where(x => x.IsEnglish == true);
            else if (lang == "ru")
                ecoSystems = ecoSystems.Where(x => x.IsEnglish == false);



            return ServiceResponse<List<EcoSystem>>.OkResponse(ecoSystems.ToList());
        }

        public ServiceResponse<EcoSystem> GetById(int id)
        {
            var ecoSystem = _context.EcoSystem
                    .Include(x => x.Photo)
                    .Include(x => x.Publications)
                    .Include(x => x.ObjectPhoto)
                    .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                    .Include(x => x.Authors).ThenInclude(x=>x.Photo)
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
                .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .FirstOrDefault(x => x.Id == id);

            var itemResponse = _mapper.Map<EcoSystemVM>(item);


            return item != null
                ? ServiceResponse<EcoSystemVM>.OkResponse(itemResponse)
                : ServiceResponse<EcoSystemVM>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<List<EcoSystem>>> Post(int userId, List<EcoSystemVM> ecoSystems)
        {
            try
            {
                var result = new List<EcoSystem>();
                foreach (var eco in ecoSystems)
                {
                    var ecoSystem = _mapper.Map<EcoSystem>(eco);
                    ecoSystem.PhotoId = eco.PhotoId;
                    ecoSystem.UserId = userId;

                    ecoSystem.LastUpdated = DateTime.Now.ToString();
                    foreach (var id in eco.Publications)
                    {
                        var publication = _context.Publication.FirstOrDefault(x => x.Id == id);
                        ecoSystem.Publications.Add(publication);
                    }

                    foreach (var id in eco.Authors)
                    {
                        var q = _context.Author.FirstOrDefault(x => x.Id == id);
                        ecoSystem.Authors.Add(q);
                    }
                    foreach (var id in eco.ObjectPhoto)
                    {
                        var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                        ecoSystem.ObjectPhoto.Add(q);
                    }
                    _context.EcoSystem.Add(ecoSystem);
                    await _context.SaveChangesAsync();
                    result.Add(ecoSystem);
                }

                if (result.Count > 1)
                    await MergeLang(result);

                return ServiceResponse<List<EcoSystem>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<EcoSystem>>.BadResponse(ex.Message);
            }
        }

        private async Task<List<EcoSystem>> MergeLang(List<EcoSystem> ecoSystems)
        {
            var ecoRu = ecoSystems.FirstOrDefault();
            var ecoEng = ecoSystems.LastOrDefault();

            ecoRu.OtherLangId = ecoEng.Id;
            ecoEng.OtherLangId = ecoRu.Id;

            _context.Update(ecoRu);
            _context.Update(ecoEng);

            _context.SaveChanges();

            return ecoSystems;
        }

        public async Task<ServiceResponse<EcoSystem>> PutVisible(int id, bool isVisible)
        {
            try
            {

                var eco = await _context.EcoSystem.FirstOrDefaultAsync(x => x.Id == id);

                if (eco == null)
                    return ServiceResponse<EcoSystem>.BadResponse("Экосистема не найдена!");

                eco.LastUpdated = DateTime.Now.ToString();
                eco.IsVisible = isVisible;

                _context.EcoSystem.Update(eco);
                _context.SaveChanges();
                return ServiceResponse<EcoSystem>.OkResponse(eco);
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

        public ServiceResponse Delete(int id)
        {
            var ecoSystem = _context.EcoSystem.Include(x=>x.ObjectPhoto)
                .Include(x=>x.Publications).Include(x=>x.SoilObjects).FirstOrDefault(x => x.Id == id);

            try
            {
                if (ecoSystem != null) _context.EcoSystem.Remove(ecoSystem);
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
