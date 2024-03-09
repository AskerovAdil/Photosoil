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

        public  ServiceResponse<List<Publication>> GetAll()
        {
            var datArticles = _context.Publication
                .Include(x => x.File)
                .Include(x => x.EcoSystems)
                .Include(x => x.SoilObjects)
                .ToList();
            return ServiceResponse<List<Publication>>.OkResponse(datArticles);
        }

        public ServiceResponse<Publication> GetById(int articleId)
        {
            var article = _context.Publication.FirstOrDefault(x=>x.Id == articleId);
            return ServiceResponse<Publication>.OkResponse(article);
        }
        public async Task<ServiceResponse<Publication>> Post(PublicationVM publicationVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(publicationVM.File?.File!);
                var file = new Core.Models.File(path, publicationVM.File.Title);

                var publication = _mapper.Map<Publication>(publicationVM);
                publication.File = file;

                _context.Publication.Add(publication);

                await _context.SaveChangesAsync();

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
                var publication = _mapper.Map<Publication>(publicationVm);

                if (publicationVm.File != null)
                {
                    var path = await FileHelper.SavePhoto(publicationVm.File.File);
                    var file = new File(path, publicationVm.File.Title);
                    publication.File = file;
                }

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
