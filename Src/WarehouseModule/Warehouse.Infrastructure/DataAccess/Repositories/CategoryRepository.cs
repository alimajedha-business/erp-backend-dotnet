using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    Repository<Category>(context),
    ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetListAsync(
        CategoryParameters categoryParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<Category>? baseQuery = null;
        if (categoryParameters.CompanyId is not null)
        {
            baseQuery = Find(w => w.CompanyId == categoryParameters.CompanyId);
        }

        return await base
            .GetList(categoryParameters, requestAdvancedFilters, baseQuery)
            .ToListAsync();
    }
}
