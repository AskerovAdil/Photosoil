using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class EcoSystemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EcoSystemService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public  ServiceResponse<List<EcoSystem>> GetAll()
        {
            var ecoSystems = _context.EcoSystem
                .Include(x=>x.Photo)
                .Include(x=>x.Publications)
                .Include(x=>x.SoilObjects)
                .ToList();
            return ServiceResponse<List<EcoSystem>>.OkResponse(ecoSystems);
        }

        public ServiceResponse<EcoSystem> GetById(int id)
        {
            var ecoSystem = _context.EcoSystem.FirstOrDefault(x=>x.Id == id);
            return ServiceResponse<EcoSystem>.OkResponse(ecoSystem);
        }
        public async Task<ServiceResponse<EcoSystem>> Post(EcoSystemVM ecoSystemVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(ecoSystemVM.Photo?.File!);
                var file = new Core.Models.File(path, ecoSystemVM.Photo.Title);

                var ecoSystem = _mapper.Map<EcoSystem>(ecoSystemVM);
                ecoSystem.Photo = file;

                _context.EcoSystem.Add(ecoSystem);

                await _context.SaveChangesAsync();

                return ServiceResponse<EcoSystem>.OkResponse(ecoSystem);
            }
            catch (Exception ex)
            {
                return ServiceResponse<EcoSystem>.BadResponse(ex.Message);
            }
        }
        public async Task<ServiceResponse<EcoSystem>> Put(int id,EcoSystemVM ecoSystemVM)
        {
            try
            {
                var ecoSystem = _mapper.Map<EcoSystem>(ecoSystemVM);
                ecoSystem.Id = id;
                if (ecoSystemVM.Photo != null)
                {
                    var path = await FileHelper.SavePhoto(ecoSystemVM.Photo.File);
                    var photo = new File(path, ecoSystemVM.Photo.Title);
                    ecoSystem.Photo = photo;
                }

                _context.EcoSystem.Update(ecoSystem);
                _context.SaveChanges();
                return ServiceResponse<EcoSystem>.OkResponse(ecoSystem);
            }
            catch (Exception ex)
            {
                return ServiceResponse<EcoSystem>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var ecoSystem = _context.EcoSystem.FirstOrDefault(x => x.Id == id);

            try
            {
                if (ecoSystem != null) _context.EcoSystem.Remove(ecoSystem);
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
