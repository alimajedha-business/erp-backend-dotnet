// Ignore Spelling: HCM

using HCM.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.DataAccess;
using HCM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCM.Infrastructure.DataAccess.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HCMDbContext context) : base(context)
        {
        }

        public void CreateDepartment(int companyId, Department department)
        {
            department.CompanyId = companyId;
            Create(department);
        }

        public void DeleteDepartment(Department department) => base.Delete(department);

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(int companyId, bool trackChanges) =>
            await FindByCondition(d => d.CompanyId.Equals(companyId), trackChanges).OrderBy(d => d.Name).ToListAsync();

        public async Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(d => ids.Contains(d.Id), trackChanges)
            .ToListAsync();

        public async Task<Department?> GetDepartmentAsync(int companyId, int departmentId, bool trackChanges) =>
            await FindByCondition(d => d.CompanyId.Equals(companyId) && d.Id.Equals(departmentId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
