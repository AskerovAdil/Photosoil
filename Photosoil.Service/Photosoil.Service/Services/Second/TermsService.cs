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
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;

namespace Photosoil.Service.Services.Second
{
    public class TermsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TermsService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<Term>> Post(TermsVM termsVm)
        {
            try
            {
                var term = _mapper.Map<Term>(termsVm);
                _context.Set<Term>().Add(term);

                var classification = _context.Classification.Include(x=>x.Terms).FirstOrDefault(x => x.Id == termsVm.ClassificationId);
                if (classification == null)
                    throw new ArgumentException("classification not found!");

                term.Classification = classification;

                var maxOrder = classification.Terms.Max(x => x.Order);
                term.Order = ++maxOrder;

                await _context.SaveChangesAsync();

                return ServiceResponse<Term>.OkResponse(term);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Term>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Term>> Put(int Id, string NameRu, string NameEng)
        {
            try
            {
                var term = _context.Term.FirstOrDefault(x => x.Id == Id);

                if(term == null)
                    return ServiceResponse<Term>.BadResponse("Термин не найден");

                term.NameRu = NameRu;
                term.NameEng = NameEng;

                _context.Term.Update(term);
                _context.SaveChanges();
                return ServiceResponse<Term>.OkResponse(term);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Term>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse> UpdateOrder(List<OrderVM> orders)
        {
            try
            {
                foreach (var el in orders)
                {
                    var term = await _context.Term.FirstOrDefaultAsync(x => x.Id == el.Id);
                    if (term == null)
                        return ServiceResponse<Classification>.BadResponse("Классификатор не найден");

                    term.Order = el.Order;
                    _context.Term.Update(term);
                }
                _context.SaveChanges();
                return ServiceResponse.OkResponse;
            }
            catch (Exception ex)
            {
                return ServiceResponse.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var term = _context.Term.Include(x=>x.Classification).
                FirstOrDefault(x => x.Id == id);

            try
            {
                if (term != null) _context.Term.Remove(term);
                var orders = _context.Classification.Include(x=>x.Terms).FirstOrDefault(x=>x.Id == term.ClassificationId)?
                    .Terms.Where(x => x.Order > term.Order);

                foreach (var el in orders) el.Order--;
                
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
