using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel.Base;

namespace Photosoil.Service.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SavePhoto(IFormFile file)
        {
            var path = $"/Storage/{Guid.NewGuid().ToString()[5..]}";
            Directory.CreateDirectory(path);
            var filepath = $"{path}/{file.FileName}";
            await using (Stream fileStream = new FileStream(filepath, FileMode.Create))
            {
                    await file.CopyToAsync(fileStream);
            }

            return filepath;

        }
    }
}
