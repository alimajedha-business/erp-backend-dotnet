using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitRepository(MainDbContext context) :
    Repository<Unit>(context),
    IUnitRepository
{
    public async Task<IReadOnlyDictionary<Guid, Unit>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken ct
    )
    {
        var requestedIds = ids
            .Distinct()
            .ToArray();

        return await _dbSet
            .AsNoTracking()
            .Where(e => requestedIds.Contains(e.Id))
            .ToDictionaryAsync(e => e.Id, ct);
    }

    public async Task<IReadOnlyDictionary<UnitDimension, Unit>> GetBaseUnitsByDimensionsAsync(
        IEnumerable<UnitDimension> dimensions,
        CancellationToken ct
    )
    {
        var requestedDimensions = dimensions
            .Distinct()
            .ToArray();

        return await _dbSet
            .AsNoTracking()
            .Where(e => e.IsBaseUnit && requestedDimensions.Contains(e.UnitDimension))
            .ToDictionaryAsync(e => e.UnitDimension, ct);
    }
}
