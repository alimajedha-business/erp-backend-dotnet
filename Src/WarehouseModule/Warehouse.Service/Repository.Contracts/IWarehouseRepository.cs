using NGErp.General.Service.Repository.Contracts;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseRepository :
    IRepositoryWithCompany<Domain.Entities.Warehouse>
{
    Task<int> GetNextCodeAsync(Guid companyId, CancellationToken ct);
}
