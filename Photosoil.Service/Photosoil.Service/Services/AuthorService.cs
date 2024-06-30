using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AuthorService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public  ServiceResponse<List<AuthorResponse>> Get(int? userId = 0,string? role = "")
        {
            var listAuthor = new List<Author>();
            if (role == "Admin" || role == "")
                listAuthor = _context.Author.Include(x => x.Photo).Include(x => x.DataRu).Include(x => x.DataEng).ToList();
            else if(role == "Moderator")
                listAuthor = _context.Author.Include(x => x.Photo).Include(x => x.DataRu).Include(x => x.DataEng)
                    .Where(x=>x.UserId == userId).ToList();


            var result = new List<AuthorResponse>();
            foreach (var el in listAuthor)
                result.Add(_mapper.Map<AuthorResponse>(el));

            return ServiceResponse<List<AuthorResponse>>.OkResponse(result);
        }

        public ServiceResponse<AuthorResponseById> GetById(int id)
        { 
                var author = _context.Author
                .Include(x => x.DataRu).Include(x => x.DataEng)
                .Include(x=>x.EcoSystems).ThenInclude(x=>x.Photo)
                .Include(x=>x.SoilObjects).ThenInclude(x=>x.Photo)
                .Include(x=>x.Photo).FirstOrDefault(x=>x.Id == id);

            var result = _mapper.Map<AuthorResponseById>(author);

           //result.Contact = JsonConvert.DeserializeObject<string[]?>(author.Contact);
           //result.OtherPrifile = JsonConvert.DeserializeObject<string[]?>(author.OtherPrifile);

            return author != null 
                ? ServiceResponse<AuthorResponseById>.OkResponse(result) 
                : ServiceResponse<AuthorResponseById>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<Author>> Post(int userId, AuthorVM authorVM)
        {
            try
            {
                var author = _mapper.Map<Author>(authorVM);
                author.PhotoId = authorVM.PhotoId;
                author.UserId = userId;

                _context.Author.Add(author);
                await _context.SaveChangesAsync();
                
                return ServiceResponse<Author>.OkResponse(author);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Author>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Author>> Put(int id, AuthorVM authorVM)
        {
            try
            {
                var author = _mapper.Map<Author>(authorVM);
                author.DataRu = authorVM.DataRu;
                author.DataEng = authorVM.DataEng;
                author.PhotoId = authorVM.PhotoId;
                author.Id = id;

                _context.Author.Update(author);
                _context.SaveChanges();
                return ServiceResponse<Author>.OkResponse(author);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Author>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var author = _context.Author.Include(x => x.DataRu).Include(x => x.DataEng).Include(x=>x.Photo).FirstOrDefault(x => x.Id == id);

            try
            {
                if (author != null) _context.Author.Remove(author);
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
