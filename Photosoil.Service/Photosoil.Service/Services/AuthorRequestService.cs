using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class AuthorRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AuthorRequestService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public ServiceResponse<AuthorRequest> Get(int id)
        {
            var author = _context.AuthorRequests.FirstOrDefault(x=>x.Id == id);

            return author != null
                ? ServiceResponse<AuthorRequest>.OkResponse(author)
                : ServiceResponse<AuthorRequest>.BadResponse(ErrorMessage.NoContent);
        }
        public ServiceResponse<List<AuthorRequest>> Get()
        {
            var authors = _context.AuthorRequests.ToList();

            return ServiceResponse<List<AuthorRequest>>.OkResponse(authors);
        }

        public async Task<ServiceResponse<AuthorRequest>> Post(AuthorRequest request)
        {
            try
            {   
                
                var authorRequest = await _context.AuthorRequests.FirstOrDefaultAsync(x=>x.Email == request.Email);
                if(authorRequest != null)
                    return ServiceResponse<AuthorRequest>.BadResponse("Заявка уже отправлена!");

                _context.AuthorRequests.Add(request);
                _context.SaveChanges();
                return ServiceResponse<AuthorRequest>.OkResponse(request);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AuthorRequest>.BadResponse(ex.Message);
            }
        }


        public ServiceResponse Delete(int id)
        {
            var authorRequest = _context.AuthorRequests
                .FirstOrDefault(x => x.Id == id);

            try
            {
                _context.Remove(authorRequest);
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
