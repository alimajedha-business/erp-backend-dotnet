// Ignore Spelling: HCM

using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(int companyId, bool trackChanges);

        Task<Department?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges);

        void CreateDepartment(int companyId,Department department);

        Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        void DeleteDepartment(Department department);
    }
}
