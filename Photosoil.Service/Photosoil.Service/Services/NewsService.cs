using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Models;
using AutoMapper;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Helpers.ViewModel.Response;

namespace Photosoil.Service.Services
{
    public class NewsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public NewsService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ServiceResponse<List<News>> GetAll(int? userId = 0, string? role = "")
        {
            IQueryable<News> news;
            if (role == "Admin" || role == "Moderator")
                news = _context.News.Include(x => x.User).Include(x => x.Files).Include(x => x.Tags).Include(x => x.Translations).Include(x => x.ObjectPhoto)
                    .AsNoTracking();
            else
                news = _context.News.Include(x => x.User).Include(x => x.Files).Include(x => x.Tags).Include(x => x.Translations.Where(x=>x.IsVisible == true))
                    .Where(x=>x.Translations.Count(x=>x.IsVisible == true) > 0).Include(x => x.ObjectPhoto).AsNoTracking();

            return ServiceResponse<List<News>>.OkResponse(news.ToList());
        }

        public ServiceResponse<List<NewsTranslation>> GetAdminAll(int? userId = 0, string? role = "")
        {
            IQueryable<NewsTranslation> soilObjects;
            if (role == "Moderator")
                soilObjects = _context.NewsTranslations.Include(x => x.News).ThenInclude(x => x.User).Where(x => x.News.UserId == userId);
            else if (role == "Admin")
                soilObjects = _context.NewsTranslations.Include(x => x.News).ThenInclude(x => x.User);
            else
                soilObjects = _context.NewsTranslations.Include(x => x.News).ThenInclude(x => x.User);

            return ServiceResponse<List<NewsTranslation>>.OkResponse(soilObjects.ToList());
        }

        public ServiceResponse<NewsResponseById> GetById(int newsId)
        {
            var news = _context.News.Include(x=>x.User).Include(x => x.Files).Include(x => x.Tags).Include(x => x.Translations).Include(x => x.ObjectPhoto)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == newsId);

            var result = _mapper.Map<NewsResponseById>(news);

            return ServiceResponse<NewsResponseById>.OkResponse(result);
        }

        public ServiceResponse<NewsVM> GetForUpdate(int id)
        {
            var item = _context.News
                .AsNoTracking()
                .Include(x => x.Files)
                .Include(x => x.Tags)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Translations)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            var itemResponse = _mapper.Map<NewsVM>(item);


            return item != null
                ? ServiceResponse<NewsVM>.OkResponse(itemResponse)
                : ServiceResponse<NewsVM>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<News>> Post(int userId, NewsVM newsVM)
        {
            try
            {
                var news = _mapper.Map<News>(newsVM);

                news.Translations.ForEach(x => x.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                news.UserId = userId;
                news.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                foreach (var id in newsVM.Tags)
                {
                    var q = _context.Tags.FirstOrDefault(x => x.Id == id);
                    news.Tags.Add(q);
                }
                foreach (var id in newsVM.Files)
                {
                    var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                    news.Files.Add(q);
                }
                foreach (var id in newsVM.ObjectPhoto)
                {
                    var q = _context.Photo.FirstOrDefault(x => x.Id == id);
                    news.ObjectPhoto.Add(q);
                }
                news.Translations = newsVM.Translations;

                _context.News.Add(news);
                await _context.SaveChangesAsync();

                return ServiceResponse<News>.OkResponse(news);
            }
            catch (Exception ex)
            {
                return ServiceResponse<News>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<NewsTranslation>> PutVisible(int id, bool isVisible)
        {
            try
            {

                var news = await _context.NewsTranslations.Include(x => x.News).FirstOrDefaultAsync(x => x.Id == id);

                if (news == null)
                    return ServiceResponse<NewsTranslation>.BadResponse("Новость не найдена!");

                news.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                news.IsVisible = isVisible;

                _context.NewsTranslations.Update(news);
                _context.SaveChanges();
                return ServiceResponse<NewsTranslation>.OkResponse(news);
            }
            catch (Exception ex)
            {
                return ServiceResponse<NewsTranslation>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse<News>> Put(int id, NewsVM newsVM)
        {
            try
            {
                var news = _context.News.Include(x => x.Files).Include(x => x.ObjectPhoto).Include(x => x.Tags)
                    .FirstOrDefault(x => x.Id == id);

                if (news == null)
                    return ServiceResponse<News>.BadResponse("Новость не найдена!");

                _mapper.Map(newsVM, news);

                var files = newsVM.Files
                     .Select(id => _context.Photo.FirstOrDefault(x => x.Id == id))
                     .ToList();
                var photos = newsVM.ObjectPhoto
                     .Select(id => _context.Photo.FirstOrDefault(x => x.Id == id))
                     .ToList();
                var tags = newsVM.Tags
                     .Select(id => _context.Tags.FirstOrDefault(x => x.Id == id))
                     .ToList();

                foreach (var el in newsVM.Translations)
                {
                    el.NewsId = id;
                    el.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    el.News = null;
                    if (_context.NewsTranslations.Any(x => x.Id == el.Id))
                        _context.NewsTranslations.Update(el);
                    else
                        _context.NewsTranslations.Add(el);
                }


                news.Files = new List<Core.Models.File>();
                news.ObjectPhoto = new List<Core.Models.File>();
                news.Tags = new List<Tag>();

                news.Files.AddRange(files);
                news.ObjectPhoto.AddRange(photos);
                news.Tags.AddRange(tags);

                _context.News.Update(news);
                _context.SaveChanges();
                return ServiceResponse<News>.OkResponse(news);
            }
            catch (Exception ex)
            {
                return ServiceResponse<News>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int TranslationId)
        {
            var translation = _context.NewsTranslations
                .Include(x => x.News).ThenInclude(x => x.Translations)
                .Include(x => x.News).ThenInclude(x => x.Tags)
                .FirstOrDefault(x => x.Id == TranslationId);

            try
            {
                if (translation != null)
                {
                    if (translation.News.Translations.Count <= 1)
                    {
                        _context.Remove(translation);
                        _context.News.Remove(translation.News);
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
