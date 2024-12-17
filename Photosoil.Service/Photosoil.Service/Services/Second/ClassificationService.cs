using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel;
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

        public ServiceResponse<Classification> GetById(int id)
        {
            var soilObjects = _context.Classification.Include(x => x.Terms)
                .FirstOrDefault(x => x.Id == id);
            return ServiceResponse<Classification>.OkResponse(soilObjects);
        }

        public async Task<ServiceResponse<Classification>> Post(ClassificationVM classificationVm)
        {
            try
            {
                var classification = _mapper.Map<Classification>(classificationVm);
                _context.Set<Classification>().Add(classification);

                foreach (var term in classificationVm.Terms)
                    classification.Terms.Add(new Term(){NameRu = term.NameRu, NameEng = term.NameEng});

                var maxOrder = _context.Classification.Max(x=>x.Order);
                classification.Order = ++maxOrder;

                await _context.SaveChangesAsync();

                return ServiceResponse<Classification>.OkResponse(classification);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Classification>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Classification>> Put(int id, string? nameRu,string? nameEng, TranslationMode TranslationMode = TranslationMode.Neutral)
        {
            try
            {
                var classification = await _context.Classification.FirstOrDefaultAsync(x => x.Id == id);
                classification.NameRu = nameRu;
                classification.NameEng = nameEng;
                classification.TranslationMode = TranslationMode;

                _context.Classification.Update(classification);
                _context.SaveChanges();
                return ServiceResponse<Classification>.OkResponse(classification);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Classification>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse> UpdateOrder(List<ClassificationOrder> orders)
        {
            try
            {
                foreach(var el in orders)
                {
                    var classification = await _context.Classification.FirstOrDefaultAsync(x => x.Id == el.Id);
                    if(classification == null)
                        return ServiceResponse<Classification>.BadResponse("Классификатор не найден");

                    classification.Order = el.Order;
                    _context.Classification.Update(classification);
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
            var classification = _context.Classification.Include(x=>x.Terms).FirstOrDefault(x => x.Id == id);

            try
            {
                if (classification != null) _context.Classification.Remove(classification);
                var orders = _context.Classification.Where(x => x.Order > classification.Order);
                foreach (var el in orders)
                    el.Order--;

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
