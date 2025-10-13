// Ignore Spelling: HCM

using HCM.Application.DTOs;
using HCM.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        public Task<DepartmentDto> CreateDepartmentForCompanyAsync(int companyId, DepartmentForCreationDto department, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDepartmentForCompanyAsync(int companyId, int departmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(int companyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int companyId, int departmentId, DepartmentForUpdateDto department, bool comTrackChanges, bool depTrackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
