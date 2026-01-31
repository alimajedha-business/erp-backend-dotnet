using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IItemRepository : IRepository<Item>
{
    Task<ListQueryResult<Item>> GetListAsync(
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
}
