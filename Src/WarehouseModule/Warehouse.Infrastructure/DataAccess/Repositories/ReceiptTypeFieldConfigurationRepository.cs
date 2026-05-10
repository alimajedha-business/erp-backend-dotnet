using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptTypeFieldConfigurationRepository(MainDbContext context) :
    Repository<ReceiptTypeFieldConfiguration>(context),
    IReceiptTypeFieldConfigurationRepository
{
    public override Task<ReceiptTypeFieldConfiguration?> SingleOrDefaultAsync(
        Expression<Func<ReceiptTypeFieldConfiguration, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return WithIncludes(query).SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<ReceiptTypeFieldConfiguration> FilterByQ(
        RequestParameters requestParameters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Filter(requestParameters);
    }

    public override IQueryable<ReceiptTypeFieldConfiguration> GetFiltered(
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Filter(requestAdvancedFilters);
    }

    private static IQueryable<ReceiptTypeFieldConfiguration> WithIncludes(
        IQueryable<ReceiptTypeFieldConfiguration> query
    )
    {
        return query
            .Include(e => e.ReceiptTypeConfiguration)
                .ThenInclude(e => e.ReceiptType)
            .Include(e => e.FieldDefinition);
    }
}
