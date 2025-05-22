using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;

namespace Photosoil.Service.Services
{
    public class AuthorRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorRequestService(ApplicationDbContext context, AuthorService authorService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _authorService = authorService;
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
                await _authorService.BecomeAuthorAsync(request);
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
