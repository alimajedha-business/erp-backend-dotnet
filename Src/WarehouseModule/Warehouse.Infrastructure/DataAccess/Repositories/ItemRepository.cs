using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemRepository(MainDbContext context) :
    Repository<Item>(context),
    IItemRepository
{
    public async Task<Item?> GetByIdAsync(Guid companyId, Guid id)
    {
        return await Find(w => w.CompanyId == companyId && w.Id == id)
            .SingleAsync();
    }

    public async Task<ListQueryResult<Item>> GetListAsync(
        Guid companyId,
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<Item>? baseQuery = Find(w => w.CompanyId == companyId);

        if (itemParameters.CategoryId is not null)
        {
            baseQuery = Find(w => w.CategoryId == itemParameters.CategoryId, baseQuery);
        }

        IQueryable<Item> sorted = base
            .GetList(requestAdvancedFilters, baseQuery)
            .Sort(itemParameters);

        var totalCount = await sorted.CountAsync();
        var items = await sorted.Paginate(itemParameters).ToListAsync();

        return new ListQueryResult<Item>(items, totalCount);
    }
}
