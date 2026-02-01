using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.Base.Service.RequestFeatures;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class DepartmentRepository(MainDbContext context) : Repository<Department>(context), IDepartmentRepository
{
    public Task<ListQueryResult<Department>> GetAllAsync(Guid companyId, DepartmentParameters departmentParameters, RequestAdvancedFilters? requestAdvancedFilters = null)
    {
        throw new NotImplementedException();
    }

    public Task<Department?> GetByIdAsync(Guid companyId, Guid id)
    {
        throw new NotImplementedException();
    }
}