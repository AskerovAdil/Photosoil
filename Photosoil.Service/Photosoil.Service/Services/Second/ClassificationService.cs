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
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;

namespace Photosoil.Service.Services.Second
{
    public class ClassificationService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClassificationService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ServiceResponse<List<Classification>> GetAll()
        {
            var soilObjects = _context.Classification.Include(x => x.Terms)
                .ToList();
            return ServiceResponse<List<Classification>>.OkResponse(soilObjects);
        }

        public ServiceResponse<List<Classification>> GetById(int id)
        {
            var soilObjects = _context.Classification.Include(x => x.Terms)
                .Where(x=>x.Id== id)
                .ToList();
            return ServiceResponse<List<Classification>>.OkResponse(soilObjects);
        }

        public async Task<ServiceResponse<Classification>> Post(ClassificationVM classificationVm)
        {
            try
            {
                var classification = _mapper.Map<Classification>(classificationVm);
                _context.Set<Classification>().Add(classification);

                foreach (var id in classificationVm.Terms)
                    classification.Terms.Add(new Term(){Name = id});

                await _context.SaveChangesAsync();

                return ServiceResponse<Classification>.OkResponse(classification);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Classification>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse<SoilObject> Put(SoilObjectVM soilObject)
        {
            try
            {
                var newSoil = _mapper.Map<SoilObject>(soilObject);


                _context.SoilObjects.Update(newSoil);
                _context.SaveChanges();
                return ServiceResponse<SoilObject>.OkResponse(newSoil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }
        public ServiceResponse Delete(int id)
        {
            var classification = _context.Classification.FirstOrDefault(x => x.Id == id);

            try
            {
                if (classification != null) _context.Classification.Remove(classification);
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
