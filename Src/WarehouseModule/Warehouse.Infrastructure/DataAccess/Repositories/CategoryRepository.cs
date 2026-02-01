using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    Repository<Category>(context),
    ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid companyId, Guid id)
    {
        return await Find(w => w.CompanyId == companyId && w.Id == id)
            .SingleAsync();
    }

    public async Task<ListQueryResult<Category>> GetListAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<Category>? baseQuery = Find(w => w.CompanyId == companyId);

        IQueryable<Category> sorted = base
            .GetList(requestAdvancedFilters, baseQuery)
            .Sort(categoryParameters);

        var totalCount = await sorted.CountAsync();
        var items = await sorted.Paginate(categoryParameters).ToListAsync();

        return new ListQueryResult<Category>(items, totalCount);
    }
}
