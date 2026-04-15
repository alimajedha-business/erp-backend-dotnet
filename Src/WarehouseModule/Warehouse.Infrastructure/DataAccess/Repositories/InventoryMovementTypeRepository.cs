using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class InventoryMovementTypeRepository(MainDbContext context) :
    RepositoryWithCompany<InventoryMovementType>(context),
    IInventoryMovementTypeRepository
{
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
