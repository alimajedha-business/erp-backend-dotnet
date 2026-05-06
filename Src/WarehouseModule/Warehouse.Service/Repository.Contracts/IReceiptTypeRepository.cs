using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IReceiptTypeRepository :
    IRepositoryWithCompany<ReceiptType>
{
    Task<int> GetNextCodeAsync(Guid companyId, CancellationToken ct);
}
