using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitOfMeasurementRepository(MainDbContext context) :
    Repository<UnitOfMeasurement>(context),
    IUnitOfMeasurementRepository
{
    public async Task<int> GetNextCodeAsync(CancellationToken ct)
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}