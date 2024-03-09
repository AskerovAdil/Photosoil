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

        ServiceResponse<List<SoilObjectResponse>> Get();
        ServiceResponse<SoilObjectResponse> GetById(int Id);
        ServiceResponse<List<SoilObjectResponse>> GetByType(SoilObjectType soilType);
        ServiceResponse<List<SoilObjectResponse>> GetByFilter(params int[] terms);

        Task<ServiceResponse<SoilObject>> Put(int id, SoilObjectVM soilObject);
        Task<ServiceResponse<SoilObject>> Post(SoilObjectVM soilObject);
        Task<ServiceResponse<SoilObject>> PostMass(SoilMass soilMass);
        ServiceResponse Delete(int Id);
        
    }
}
