using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IReceiptSourceOfSupplyRepository :
    IRepositoryWithCompany<ReceiptSourceOfSupply>
{
    Task<bool> HasReceiptFieldValueReferencesAsync(
        Guid companyId,
        Guid receiptSourceOfSupplyId,
        CancellationToken ct
    );

    Task<int> GetNextCodeAsync(
        Guid companyId,
        CancellationToken ct
    );
}
