using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptTypeFieldConfigurationRepository(MainDbContext context) :
    RepositoryWithCompany<ReceiptTypeFieldConfiguration>(context),
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
        Guid companyId,
        RequestParameters requestParameters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestParameters);
    }

    public override IQueryable<ReceiptTypeFieldConfiguration> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    private static IQueryable<ReceiptTypeFieldConfiguration> WithIncludes(
        IQueryable<ReceiptTypeFieldConfiguration> query
    )
    {
        return query
            .Include(e => e.ReceiptType)
            .Include(e => e.FieldDefinition);
    }
}
