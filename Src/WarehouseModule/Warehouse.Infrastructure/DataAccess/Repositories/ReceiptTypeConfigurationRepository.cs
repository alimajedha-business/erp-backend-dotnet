using System.Linq.Expressions;

using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptTypeConfigurationRepository(MainDbContext context) :
    RepositoryWithCompany<ReceiptTypeConfiguration>(context),
    IReceiptTypeConfigurationRepository
{
    public override Task<ReceiptTypeConfiguration?> SingleOrDefaultAsync(
        Expression<Func<ReceiptTypeConfiguration, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return WithIncludes(query).SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<ReceiptTypeConfiguration> FilterByQ(
        Guid companyId,
        RequestParameters requestParameters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestParameters);
    }

    public override IQueryable<ReceiptTypeConfiguration> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    private static IQueryable<ReceiptTypeConfiguration> WithIncludes(
        IQueryable<ReceiptTypeConfiguration> query
    )
    {
        return query
            .Include(e => e.ReceiptType)
            .Include(e => e.FieldConfigurations)
                .ThenInclude(e => e.FieldDefinition);
    }
}
