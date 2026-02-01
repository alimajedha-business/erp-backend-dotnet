using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    RepositoryWithCompany<Category>(context),
    ICategoryRepository
{
    public async Task<ListQueryResult<Category>> GetAllAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<Category> sorted = base
            .GetAll(companyId, requestAdvancedFilters)
            .Sort(categoryParameters);

        var totalCount = await sorted.CountAsync();
        var items = await sorted.Paginate(categoryParameters).ToListAsync(ct);

        return new ListQueryResult<Category>(items, totalCount);
    }
}
