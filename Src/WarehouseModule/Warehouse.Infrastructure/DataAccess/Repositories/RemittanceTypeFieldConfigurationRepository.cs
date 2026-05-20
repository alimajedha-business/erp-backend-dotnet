using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class RemittanceTypeFieldConfigurationRepository(MainDbContext context) :
    Repository<RemittanceTypeFieldConfiguration>(context),
    IRemittanceTypeFieldConfigurationRepository
{
    public override Task<RemittanceTypeFieldConfiguration?> SingleOrDefaultAsync(
        Expression<Func<RemittanceTypeFieldConfiguration, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return WithIncludes(query).SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<RemittanceTypeFieldConfiguration> FilterByQ(
        RequestParameters requestParameters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Filter(requestParameters);
    }

    public override IQueryable<RemittanceTypeFieldConfiguration> GetFiltered(
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Filter(requestAdvancedFilters);
    }

    private static IQueryable<RemittanceTypeFieldConfiguration> WithIncludes(
        IQueryable<RemittanceTypeFieldConfiguration> query
    )
    {
        return query
            .Include(e => e.RemittanceTypeConfiguration)
                .ThenInclude(e => e.RemittanceType)
            .Include(e => e.FieldDefinition);
    }
}
