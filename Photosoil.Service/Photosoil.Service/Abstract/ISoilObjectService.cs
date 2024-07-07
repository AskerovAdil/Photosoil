using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Core.Enum;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Helpers.ViewModel.Response;
using Photosoil.Service.Helpers.ViewModel.Request;

namespace Photosoil.Service.Abstract
{
    public interface ISoilObjectService
    {

        ServiceResponse<List<SoilResponse>> Get(int? userId = 0, string? role = "");
        ServiceResponse<List<SoilTranslation>> GetAdminAll(int? userId = 0, string? role = "");
        ServiceResponse<SoilResponseById> GetById(int Id);
        ServiceResponse<SoilObjectVM> GetForUpdate(int id);
        ServiceResponse<List<SoilResponse>> GetByType(SoilObjectType soilType);
        ServiceResponse<List<SoilResponse>> GetByFilter(params int[] terms);

        Task<ServiceResponse<SoilObject>> Put(int id, SoilObjectVM soilObject);
        Task<ServiceResponse<SoilObject>> PutVisible(int id, bool isVisible);
        Task<ServiceResponse<SoilObject>> Post(int userId, SoilObjectVM soil);
        Task<ServiceResponse<SoilObject>> PostMass(SoilMass soilMass);
        ServiceResponse Delete(int Id);
        
    }
}
