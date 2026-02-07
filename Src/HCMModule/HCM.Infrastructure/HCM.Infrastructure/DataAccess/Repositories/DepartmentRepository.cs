
using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class DepartmentRepository(MainDbContext context) :
    RepositoryWithCompany<Department>(context),
    IDepartmentRepository
{
    public async Task<ListQueryResult<Department>> GetAllAsync(
        Guid companyId,
        DepartmentParameters departmentParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
        )
    {
        IQueryable<Department> sorted = base
            .GetAll(companyId, requestAdvancedFilters)
            .Sort(departmentParameters);

        var totalCount = await sorted.CountAsync(ct);
        var items = await sorted.Paginate(departmentParameters).ToListAsync(ct);

        return new ListQueryResult<Department>(items, totalCount);
    }
}