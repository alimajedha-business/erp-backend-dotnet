using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseTypeRepository : IRepository<WarehouseType>
{
    Task<int> GetNextCodeAsync(CancellationToken ct);
}
