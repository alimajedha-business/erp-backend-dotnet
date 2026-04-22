using System.Linq.Expressions;

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
    public override async Task<Domain.Entities.Warehouse?> SingleOrDefaultAsync(
        Expression<Func<Domain.Entities.Warehouse, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .AsSplitQuery();

        return await query
            .Include(i => i.WarehouseType)
            .Include(i => i.CompanyUnit)
            .SingleOrDefaultAsync(predicate, ct);
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