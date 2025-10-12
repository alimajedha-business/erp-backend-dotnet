// Ignore Spelling: HCM

using HCM.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.DataAccess;
using HCM.Domain.Entities;

namespace HCM.Infrastructure.DataAccess.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HCMDbContext context) : base(context)
        {
        }

        public void CreateDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync(int companyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
