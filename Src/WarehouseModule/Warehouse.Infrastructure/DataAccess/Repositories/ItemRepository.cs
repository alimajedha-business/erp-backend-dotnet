using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemRepository(MainDbContext context) :
    RepositoryWithCompany<Item>(context),
    IItemRepository
{
    public async Task<ListQueryResult<Item>> GetAllAsync(
        Guid companyId,
        ItemParameters itemParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<Item>? baseQuery = null;
        if (itemParameters.CategoryId is not null)
        {
            baseQuery = Find(w => w.CategoryId == itemParameters.CategoryId);
        }

        IQueryable<Item> sorted = base
            .GetAll(companyId, requestAdvancedFilters, baseQuery)
            .Sort(itemParameters);

        var totalCount = await sorted.CountAsync(ct);
        var items = await sorted.Paginate(itemParameters).ToListAsync(ct);

        return new ListQueryResult<Item>(items, totalCount);
    }
}
