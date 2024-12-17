using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;

namespace Photosoil.Service.Services.Second
{
    public class TagsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TagsService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public ServiceResponse<List<Tag>> GetAll()
        {
            var tags = _context.Tags
                .ToList();
            return ServiceResponse<List<Tag>>.OkResponse(tags);
        }
        public async Task<ServiceResponse<Tag>> Post(string NameRu,string NameEng)
        {
            try
            {
                var tag = new Tag() { NameRu = NameRu, NameEng = NameEng };
                _context.Set<Tag>().Add(tag);
                await _context.SaveChangesAsync();

                return ServiceResponse<Tag>.OkResponse(tag);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Tag>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Tag>> Put(int Id, string NameRu, string NameEng)
        {
            try
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == Id);

                if(tag == null)
                    return ServiceResponse<Tag>.BadResponse("Тэг не найден");

                tag.NameRu = NameRu;
                tag.NameEng = NameEng;

                _context.Tags.Update(tag);
                _context.SaveChanges();
                return ServiceResponse<Tag>.OkResponse(tag);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Tag>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var tag = _context.Tags.
                FirstOrDefault(x => x.Id == id);

            try
            {

                if (tag != null) _context.Tags.Remove(tag);
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
