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
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
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

        public  ServiceResponse<List<Publication>> GetAll(int? userId = 0, string? role = "")
        {
            var datArticles = new List<Publication>();
            if (role == "")
                datArticles = _context.Publication.Include(x => x.File).Include(x => x.EcoSystems).Include(x => x.SoilObjects)
                    .Where(x=>x.IsVisible == true).ToList();
            else if (role == "Admin")
                datArticles = _context.Publication.Include(x => x.File).Include(x => x.EcoSystems).Include(x => x.SoilObjects)
                    .ToList();
            else if (role == "Moderator")
                datArticles = _context.Publication.Include(x => x.File).Include(x => x.EcoSystems).Include(x => x.SoilObjects)
                    .Where(x=>x.UserId == userId).ToList();


            return ServiceResponse<List<Publication>>.OkResponse(datArticles);
        }

        public ServiceResponse<PublicationResponseById> GetById(int articleId)
        {
            var article = _context.Publication.Include(x=>x.File)
                .Include (x => x.EcoSystems)
                .Include(x=>x.SoilObjects)
                .FirstOrDefault(x=>x.Id == articleId);

            var result = _mapper.Map<PublicationResponseById>(article); 

            return ServiceResponse<PublicationResponseById>.OkResponse(result);
        }

        public ServiceResponse<PublicationVM> GetForUpdate(int id)
        {
            var soilObject = _context.Publication
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            var soilObjectResponse = _mapper.Map<PublicationVM>(soilObject);


            return soilObject != null
                ? ServiceResponse<PublicationVM>.OkResponse(soilObjectResponse)
                : ServiceResponse<PublicationVM>.BadResponse(ErrorMessage.NoContent);
        }


        public async Task<ServiceResponse<List<Publication>>> Post(int userId, List<PublicationVM> publicationVM)
        {
            try
            {
                var result = new List<Publication>();

                foreach (var el in publicationVM)
                {
                    var publication = _mapper.Map<Publication>(el);
                    publication.FileId = el.FileId;
                    publication.UserId = userId;

                    publication.LastUpdated = DateTime.Now.ToString();

                    _context.Publication.Add(publication);
                    await _context.SaveChangesAsync();

                    result.Add(publication);

                }
                if (result.Count > 1)
                    await MergeLang(result);

                return ServiceResponse<List<Publication>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<Publication>>.BadResponse(ex.Message);
            }
        }


        private async Task<List<Publication>> MergeLang(List<Publication> publications)
        {
            var Ru = publications.FirstOrDefault();
            var Eng = publications.LastOrDefault();

            Ru.OtherLangId = Eng.Id;
            Eng.OtherLangId = Ru.Id;

            _context.Update(Ru);
            _context.Update(Eng);

            _context.SaveChanges();

            return publications;
        }


        public async Task<ServiceResponse<Publication>> PutVisible(int id, bool isVisible)
        {
            try
            {
                var publication = await _context.Publication
                    .FirstOrDefaultAsync(x => x.Id == id);

                publication.LastUpdated = DateTime.Now.ToString();
                publication.IsVisible = isVisible;

                if (publication == null)
                    return ServiceResponse<Publication>.BadResponse("Публикация не найдена!");

                _context.Publication.Update(publication);
                _context.SaveChanges();
                return ServiceResponse<Publication>.OkResponse(publication);
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
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                publication.LastUpdated = DateTime.Now.ToString();
                
                publication.FileId = publicationVm.FileId;

                if (publication == null)
                    return ServiceResponse<Publication>.BadResponse("Публикация не найдена!");


               //if (publicationVm.File?.File != null)
               //{
               //    var path = await FileHelper.SavePhoto(publicationVm.File.File);
               //    var file = new File(path, publicationVm.File.Title);
               //    publication.File = file;
               //}

                _mapper.Map(publicationVm, publication);

                _context.Publication.Update(publication);
                _context.SaveChanges();
                return ServiceResponse<Publication>.OkResponse(publication);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Publication>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var publication = _context.Publication.FirstOrDefault(x => x.Id == id);

            try
            {
                if (publication != null) _context.Publication.Remove(publication);
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
