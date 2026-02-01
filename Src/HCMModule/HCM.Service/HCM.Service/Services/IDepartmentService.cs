using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Service.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartmentsAsync(
            Guid companyId, DepartmentParameters departmentParameters, 
            RequestAdvancedFilters? requestAdvancedFilters = null);
        Task<DepartmentDto?> GetDepartmentByIdAsync(Guid companyId, Guid id);
        Task<DepartmentDto> CreateDepartmentAsync(Guid companyId, CreateDepartmentDto dto);
        Task<DepartmentDto> UpdateDepartmentAsync(Guid companyId, Guid id, UpdateDepartmentDto dto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
