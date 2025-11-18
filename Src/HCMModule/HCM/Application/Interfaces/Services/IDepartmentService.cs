using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCM.Application.DTOs;

namespace HCM.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(int companyId, bool trackChanges);
        Task<DepartmentDto?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges);
        Task<DepartmentDto> CreateDepartmentForCompanyAsync(int companyId, DepartmentForCreationDto departmentDto, bool trackChanges);
        Task DeleteDepartmentForCompanyAsync(int companyId, int departmentId, bool trackChanges);
        Task UpdateAsync(int companyId, int departmentId, DepartmentForUpdateDto departmentDto, bool comTrackChanges,
            bool depTrackChanges);
    }
}
