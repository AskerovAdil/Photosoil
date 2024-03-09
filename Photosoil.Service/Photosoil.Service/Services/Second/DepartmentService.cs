using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Base;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Abstract;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel;

namespace Photosoil.Service.Services.Second
{

    public class DepartmentService <T> where T : BaseSecond, new()
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ServiceResponse<List<T>> GetAll()
        {

            var elDepartments = _context.Set<T>().ToList();
            return ServiceResponse<List<T>>.OkResponse(elDepartments);
        }

        public async Task<ServiceResponse<T>> Post(T Entity)
        {
            try
            {
                _context.Set<T>().Add(Entity);
                await _context.SaveChangesAsync();

                return ServiceResponse<T>.OkResponse(Entity);
            }
            catch (Exception ex)
            {
                return ServiceResponse<T>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<T>> Post(string Name)
        {
            try
            {
                var soilDepartment = new T(){Name = Name};


                _context.Set<T>().Add(soilDepartment);
                await _context.SaveChangesAsync();

                return ServiceResponse<T>.OkResponse(soilDepartment);
            }
            catch (Exception ex)
            {
                return ServiceResponse<T>.BadResponse(ex.Message);
            }
        }
        public ServiceResponse<T> Put(T soil)
        {
            try
            {
                _context.Set<T>().Update(soil);
                _context.SaveChanges();
                return ServiceResponse<T>.OkResponse(soil);
            }
            catch (Exception ex)
            {
                return ServiceResponse<T>.BadResponse(ex.Message);
            }
        }
        public ServiceResponse Delete(int id)
        {
            var soilObject = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            try
            {
                if (soilObject != null) _context.Set<T>().Remove(soilObject);
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
