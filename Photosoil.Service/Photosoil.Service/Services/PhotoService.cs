using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using Photosoil.Service.Helpers.ViewModel.Base;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;

namespace Photosoil.Service.Services
{
    public class PhotoService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PhotoService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public  ServiceResponse<List<File>> Get(int soilId)
        {

            var soilPhoto = _context.SoilObjects.Include(x=>x.ObjectPhoto).FirstOrDefault(x => x.Id == soilId)?.ObjectPhoto;
            
            return ServiceResponse<List<Core.Models.File>>.OkResponse(soilPhoto);
        }
        
        public async Task<ServiceResponse<Core.Models.File>> Post(PhotoVM photoVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(photoVM.Photo);
                var photo = new Core.Models.File(path, photoVM.Title);
                
                var soil = _context.SoilObjects.First(x => x.Id == photoVM.soilId);
                soil.ObjectPhoto.Add(photo);

                _context.Photo.AddRange(photo);
                await _context.SaveChangesAsync();

                return ServiceResponse<Core.Models.File>.OkResponse(photo);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Core.Models.File>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<File>> Put(int id,string Title)
        {
            try
            {
                var photo = _context.Photo.FirstOrDefault(x => x.Id == id);
                photo.Title = Title;
                _context.Photo.Update(photo);
                _context.SaveChanges();
                
                return ServiceResponse<File>.OkResponse(photo);
            }
            catch (Exception ex)
            {
                return ServiceResponse<File>.BadResponse(ex.Message);
            }
        }
        public ServiceResponse Delete(int photoId)
        {
            var photo = _context.Photo.FirstOrDefault(x => x.Id == photoId);

            try
            {
                if (photo != null) _context.Photo.Remove(photo);
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
