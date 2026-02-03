using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    RepositoryWithCompany<Category>(context),
    ICategoryRepository
{
    public async Task<ListQueryResult<Category>> GetDirectChildrenAsync(
        Guid companyId,
        Guid id,
        CategoryParameters categoryParameters,
        CancellationToken ct
    )
    {
        IQueryable<Category> query = Find(e => e.ParentCategoryId == id);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Paginate(categoryParameters)
            .Sort(categoryParameters)
            .ToListAsync(ct);

        return new ListQueryResult<Category>(items, totalCount);
    }
}
