using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IItemRepository : IRepository<Item>
{
    Task<IEnumerable<Item>> GetPaginatedAsync(
        ItemParameters prms,
        string? search = null,
        object[]? searchPrms = null
    );
}
