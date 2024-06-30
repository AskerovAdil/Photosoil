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
    public class ArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ArticleService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public  ServiceResponse<List<Article>> GetAll()
        {

            var datArticles = _context.Article.ToList();
            return ServiceResponse<List<Article>>.OkResponse(datArticles);
        }

        public ServiceResponse<Article> GetById(int articleId)
        {
            var article = _context.Article.FirstOrDefault(x=>x.Id == articleId);
            return ServiceResponse<Article>.OkResponse(article);
        }
        public async Task<ServiceResponse<Article>> Post(ArticleVM articleVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(articleVM.Photo.File);
                var photo = new Core.Models.File(path, articleVM.Photo.TitleEng, articleVM.Photo.TitleRu);

                var article = _mapper.Map<Article>(articleVM);
                article.Photo = photo;

                _context.Article.Add(article);

                await _context.SaveChangesAsync();

                return ServiceResponse<Article>.OkResponse(article);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Article>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Article>> Put(ArticleVM articleVM)
        {
            try
            {
                var article = _mapper.Map<Article>(articleVM);

                if (articleVM.Photo != null)
                {
                    var path = await FileHelper.SavePhoto(articleVM.Photo.File);
                    var photo = new Core.Models.File(path, articleVM.Photo.TitleEng, articleVM.Photo.TitleRu);
                    article.Photo = photo;
                }



                _context.Article.Update(article);
                _context.SaveChanges();
                return ServiceResponse<Article>.OkResponse(article);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Article>.BadResponse(ex.Message);
            }
        }


        public ServiceResponse Delete(int photoId)
        {
            var photo = _context.Photo.FirstOrDefault(x => x.Id == photoId);

            try
            {
                if (photo != null) _context.Photo.Remove(photo);
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
