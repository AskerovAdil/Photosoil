using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using File = Photosoil.Core.Models.File;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;

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

        public ServiceResponse<List<File>> GetAll ()
        {

            var soilPhoto = _context.Photo.ToList();

            return ServiceResponse<List<Core.Models.File>>.OkResponse(soilPhoto);
        }

        public ServiceResponse<List<File>> GetBySoilId(int soilId)
        {

            var soilPhoto = _context.SoilObjects.Include(x=>x.ObjectPhoto).FirstOrDefault(x => x.Id == soilId)?.ObjectPhoto;
            
            return ServiceResponse<List<Core.Models.File>>.OkResponse(soilPhoto);
        }

        public ServiceResponse<File> GetById(int Id)
        {

            var soilPhoto = _context.Photo.FirstOrDefault(x => x.Id == Id);

            return ServiceResponse<File>.OkResponse(soilPhoto);
        }

        public async Task<ServiceResponse<Core.Models.File>> Post(PhotoVM photoVM)
        {
            try
            {
                var path = await FileHelper.SavePhoto(photoVM.Photo);
                var photo = new File();
                if (photoVM.Photo.ContentType.Contains("image"))
                {
                    var pahtResize = await CompressAndSaveImageAsync(photoVM.Photo, path);
                    photo.PathResize = pahtResize; 
                }
                photo.TitleEng = photoVM.TitleEng;
                photo.TitleRu= photoVM.TitleRu;
                photo.Path = path;


                _context.Photo.AddRange(photo);
                await _context.SaveChangesAsync();

                return ServiceResponse<File>.OkResponse(photo);
            }
            catch (Exception ex)
            {
                return ServiceResponse<File>.BadResponse(ex.Message);
            }
        }

        public async Task<string> CompressAndSaveImageAsync(IFormFile imageFile, string path, int quality = 10)
        {
            string compressedPath = Path.Combine(
                Path.GetDirectoryName(path),
                $"resize-{Path.GetFileNameWithoutExtension(path)}{Path.GetExtension(path)}"
            ).Replace("\\", "/");

            using var imageStream = imageFile.OpenReadStream();
            using var image = Image.Load(imageStream);
            var encoder = new JpegEncoder
            {
                Quality = quality, 
            };

            await Task.Run(() => image.Save(compressedPath, encoder));
            return compressedPath;
        }

        public async Task<ServiceResponse<File>> Put(int id,string? TitleEng, string? TitleRu)
        {
            try
            {
                var photo = _context.Photo.FirstOrDefault(x => x.Id == id);
                photo.TitleEng = TitleEng;
                photo.TitleRu = TitleRu;
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
