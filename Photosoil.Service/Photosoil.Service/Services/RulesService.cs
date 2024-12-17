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
    public class RulesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RulesService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ServiceResponse<Rules> Get()
        {
            var rules = _context.Rules.Include(x => x.Files).FirstOrDefault();
            if (rules == null) {

                var rule = new Rules()
                {
                    ContentRu = "",
                    ContentEng = ""
                };
                _context.Add(rule);
                _context.SaveChanges();
            }


            return ServiceResponse<Rules>.OkResponse(rules);
        }

        public async Task<ServiceResponse<Rules>> Put(RulesVM rulesVM)
        {
            try
            {   
                
                var rule = await _context.Rules.Include(x=>x.Files).FirstOrDefaultAsync();
                
                if (rule == null)
                {
                    rule = new Rules()
                    {
                        ContentRu = rulesVM.ContentRu,
                        ContentEng = rulesVM.ContentEng
                    };
                    _context.Add(rule);
                }

                _mapper.Map(rulesVM, rule);

                var photo= rulesVM.Files
                     .Select(id => _context.Photo.FirstOrDefault(x => x.Id == id))
                     .ToList();
           

                rule.Files = new List<File>();
                rule.Files.AddRange(photo);

                _context.Rules.Update(rule);
                 _context.SaveChanges();
                return ServiceResponse<Rules>.OkResponse(rule);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Rules>.BadResponse(ex.Message);
            }
        }

        private void CreateRules()
        {
            var rule = new Rules()
            {
                ContentRu = "",
                ContentEng = ""
            };
            _context.Add(rule);
            _context.SaveChanges();
        }
    }
}
