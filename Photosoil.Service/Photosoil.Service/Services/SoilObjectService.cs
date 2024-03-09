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
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class SoilObjectService : ISoilObjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SoilObjectService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public  ServiceResponse<List<SoilObjectResponse>> Get()
        {
            var soilObjects = _context.SoilObjects.Include(x=>x.ObjectPhoto)
                .Include(x => x.Author)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x=>x.Classification)
                .ToList();

            var soilObjectsResponse = new List<SoilObjectResponse>();

            foreach (var soilObject in soilObjects)
            {
                var classificationVMs = soilObject.Terms
                    .GroupBy(t => t.ClassificationId)
                    .Select(g => new ClassificationResponse
                    {
                        Name = g.First().Classification.Name,
                        Terms = g.Select(t => _mapper.Map<TermsResponse>(t)).ToList()
                    })
                    .ToList();
                var soilObjectResponse = _mapper.Map<SoilObjectResponse>(soilObject);
                soilObjectResponse.Classification = classificationVMs;

                soilObjectsResponse.Add(soilObjectResponse);
            }



            return ServiceResponse<List<SoilObjectResponse>>.OkResponse(soilObjectsResponse);
        }

        public ServiceResponse<SoilObjectResponse>GetById(int id)
        {
            var soilObject =_context.SoilObjects

                .AsNoTracking()
                .Include(x=>x.Photo)
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Author)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x=>x.Classification)
                .FirstOrDefault(x=>x.Id == id);

            var soilObjectResponse = _mapper.Map<SoilObjectResponse>(soilObject);

            var classificationVMs = soilObject.Terms
                .GroupBy(t => t.ClassificationId)
                .Select(g => new ClassificationResponse
                {
                    Name = g.First().Classification.Name,
                    Terms = g.Select(t=>_mapper.Map<TermsResponse>(t)).ToList()
                })
                .ToList();



            soilObjectResponse.Classification = classificationVMs;

            return soilObject!=null 
                ? ServiceResponse<SoilObjectResponse>.OkResponse(soilObjectResponse) 
                : ServiceResponse<SoilObjectResponse>.BadResponse(ErrorMessage.NoContent);
        }
        public ServiceResponse<List<SoilObjectResponse>> GetByType(SoilObjectType soilType)
        {

            var soilObjects = _context.SoilObjects.Where(x => x.ObjectType == soilType).Include(x => x.ObjectPhoto)
                .Include(x => x.Author)
                .Include(x=>x.EcoSystems).ThenInclude(x=>x.Photo)
                .Include(x=>x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x => x.Classification)
                .ToList();

            var soilObjectsResponse = ConvretResponseObject(soilObjects);


            return ServiceResponse<List<SoilObjectResponse>>.OkResponse(soilObjectsResponse);

        }

        public ServiceResponse<List<SoilObjectResponse>> GetByFilter(params int[] ids)
        {
            var soils = _context.SoilObjects
                .Include(x => x.ObjectPhoto)
                .Include(x => x.Author)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.Publications).ThenInclude(x => x.File)
                .Include(x => x.Terms).ThenInclude(x => x.Classification)
                .ToList();

            var soilObjects = 
                soils.Where(soilObject => ids.All(x => soilObject.Terms.Exists(term => term.Id == x))).ToList();

            var soilObjectsResponse = ConvretResponseObject(soilObjects);



            return ServiceResponse<List<SoilObjectResponse>>.OkResponse(soilObjectsResponse);

        }
        private List<SoilObjectResponse> ConvretResponseObject(List<SoilObject> soilObjects)
        {
            var soilObjectsResponse = new List<SoilObjectResponse>();

            foreach (var soilObject in soilObjects)
            {
                var classificationVMs = soilObject.Terms
                    .GroupBy(t => t.ClassificationId)
                    .Select(g => new ClassificationResponse
                    {
                        Name = g.First().Classification.Name,
                        Terms = g.Select(t => _mapper.Map<TermsResponse>(t)).ToList()
                    })
                    .ToList();
                var soilObjectResponse = _mapper.Map<SoilObjectResponse>(soilObject);
                soilObjectResponse.Classification = classificationVMs;

                soilObjectsResponse.Add(soilObjectResponse);
            }

            return soilObjectsResponse;
        }
        public async Task<ServiceResponse<SoilObject>> Post(SoilObjectVM soil)
        {
            try
            {
                var path = await FileHelper.SavePhoto(soil.Photo.File);
                var photo = new Core.Models.File(path, soil.Photo.Title);
                var newSoil = _mapper.Map<SoilObject>(soil);

                foreach (var id in soil.SoilTerms)
                {
                    var q = _context.Term.FirstOrDefault(x => x.Id == id);
                    newSoil.Terms.Add(q);
                }
                foreach (var id in soil.Publications)
                {
                    var q = _context.Publication.FirstOrDefault(x => x.Id == id);
                    newSoil.Publications.Add(q);
                }
                foreach (var id in soil.EcoSystems)
                {
                    var q = _context.EcoSystem.FirstOrDefault(x => x.Id == id);
                    newSoil.EcoSystems.Add(q);
                }
                newSoil.Photo = photo;
                _context.SoilObjects.Add(newSoil);
                await _context.SaveChangesAsync();
                
                return ServiceResponse<SoilObject>.OkResponse(newSoil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<SoilObject>> Put(int id, SoilObjectVM soilObjectVm)
        {
            try
            {
                await _context.EcoSystem.ForEachAsync(x => x.SoilObjects.RemoveAll(x => x.Id == id));
                await _context.Publication.ForEachAsync(x => x.SoilObjects.RemoveAll(x => x.Id == id));
                await _context.Term.ForEachAsync(x => x.SoilObjects.RemoveAll(x => x.Id == id));


                await _context.SaveChangesAsync();

                var soilObject = _mapper.Map<SoilObject>(soilObjectVm);
                soilObject.Id = id;


                if (soilObjectVm.Photo != null)
                {
                    var path = await FileHelper.SavePhoto(soilObjectVm.Photo.File);
                    var file = new File(path, soilObjectVm.Photo.Title);
                    soilObject.Photo = file;
                }


                var newTerms = soilObjectVm.SoilTerms
                    .Select(idSoilTerm => _context.Term.FirstOrDefault(x => x.Id == idSoilTerm))
                    .ToList();

                soilObject.Terms.AddRange(newTerms);


                var newPublication= soilObjectVm.Publications
                    .Select(id=> _context.Publication.FirstOrDefault(x => x.Id == id))
                    .ToList();

                soilObject.Publications.AddRange(newPublication);

                var newSystem= soilObjectVm.EcoSystems
                    .Select(id => _context.EcoSystem.FirstOrDefault(x => x.Id == id))
                    .ToList();

                soilObject.EcoSystems.AddRange(newSystem);

           

                _context.SoilObjects.Update(soilObject);
                await _context.SaveChangesAsync();
                return ServiceResponse<SoilObject>.OkResponse(soilObject);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse<SoilObject>> PostMass(SoilMass soil)
        {
            try
            {
                var path = await FileHelper.SavePhoto(soil.Photo.File);
                var photo = new Core.Models.File(path, soil.Photo.Title);

                var newSoil = _mapper.Map<SoilObject>(soil);
                newSoil.Photo = photo;
                _context.SoilObjects.Add(newSoil);
                await _context.SaveChangesAsync();

                return ServiceResponse<SoilObject>.OkResponse(newSoil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SoilObject>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var soilObject = _context.SoilObjects.FirstOrDefault(x => x.Id == id);

            try
            {
                if (soilObject != null) _context.SoilObjects.Remove(soilObject);
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
