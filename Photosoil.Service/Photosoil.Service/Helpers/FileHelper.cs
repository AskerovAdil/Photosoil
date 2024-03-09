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
            await using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                    await file.CopyToAsync(fileStream);
            }

            return $"{path}/{file.FileName}";

        }
    }
}
