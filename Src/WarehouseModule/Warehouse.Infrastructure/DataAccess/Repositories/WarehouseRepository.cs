using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseRepository(MainDbContext context) :
    RepositoryWithCompany<Domain.Entities.Warehouse>(context),
    IWarehouseRepository
{
    public override Task<Domain.Entities.Warehouse?> GetByIdAsync(
    Guid id,
    bool trackChanges = false,
    CancellationToken ct = default
)
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return query
            .Include(i => i.WarehouseType)
            .Include(i => i.CompanyUnit)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public override IQueryable<Domain.Entities.Warehouse> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        var query = base.GetFiltered(companyId, requestAdvancedFilters);
        return query
            .Include(i => i.CompanyUnit)
            .Include(i => i.WarehouseType);
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