using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptSourceOfSupplyRepository(MainDbContext context) :
    RepositoryWithCompany<ReceiptSourceOfSupply>(context),
    IReceiptSourceOfSupplyRepository
{
    public Task<bool> HasReceiptFieldValueReferencesAsync(
        Guid companyId,
        Guid receiptSourceOfSupplyId,
        CancellationToken ct
    )
    {
        return _context
            .Set<ReceiptFieldValue>()
            .AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.ReferenceId == receiptSourceOfSupplyId,
                ct
            );
    }

    public async Task<int> GetNextCodeAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}
