using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitOfMeasurementRepository(MainDbContext context) :
    Repository<UnitOfMeasurement>(context),
    IUnitOfMeasurementRepository
{
    public override async Task<UnitOfMeasurement?> SingleOrDefaultAsync(
        Expression<Func<UnitOfMeasurement, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .AsSplitQuery();

        return await query
            .Include(i => i.MeasurementDimension)
            .SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<UnitOfMeasurement> GetFiltered(
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        var query = base.GetFiltered(requestAdvancedFilters);
        return query.Include(i => i.MeasurementDimension);
    }

    public async Task<int> GetNextCodeAsync(CancellationToken ct)
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}