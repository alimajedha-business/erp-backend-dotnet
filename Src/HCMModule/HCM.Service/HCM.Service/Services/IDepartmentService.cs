using NGErp.HCM.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Service.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
