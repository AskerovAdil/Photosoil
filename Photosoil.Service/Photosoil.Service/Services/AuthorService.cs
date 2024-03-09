using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
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

        public  ServiceResponse<List<Author>> Get()
        {
            var authors = _context.Author.Include(x=>x.Photo).ToList();
            return ServiceResponse<List<Author>>.OkResponse(authors);
        }

        public ServiceResponse<Author> GetById(int id)
        { 
            var author = _context.Author.Include(x=>x.Photo).FirstOrDefault(x=>x.Id == id);

            return author != null 
                ? ServiceResponse<Author>.OkResponse(author) 
                : ServiceResponse<Author>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<Author>> Post(AuthorVM authorVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(authorVM.Photo.File);
                var photo = new Core.Models.File(path, authorVM.Photo.Title);
                var author = _mapper.Map<Author>(authorVM);
                author.Photo = photo;

                _context.Author.Add(author);
                await _context.SaveChangesAsync();
                
                return ServiceResponse<Author>.OkResponse(author);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Author>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Author>> Put(AuthorVM authorVM)
        {
            try
            {
                var author = _mapper.Map<Author>(authorVM);

                if (authorVM.Photo != null)
                {
                    var path = await FileHelper.SavePhoto(authorVM.Photo.File);
                    var photo = new File(path, authorVM.Photo.Title);
                    author.Photo = photo;
                }

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
            var author = _context.Author.FirstOrDefault(x => x.Id == id);

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
