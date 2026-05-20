using System.Linq.Expressions;

using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class RemittanceTypeConfigurationRepository(MainDbContext context) :
    RepositoryWithCompany<RemittanceTypeConfiguration>(context),
    IRemittanceTypeConfigurationRepository
{
    public override Task<RemittanceTypeConfiguration?> SingleOrDefaultAsync(
        Expression<Func<RemittanceTypeConfiguration, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return WithIncludes(query).SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<RemittanceTypeConfiguration> FilterByQ(
        Guid companyId,
        RequestParameters requestParameters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestParameters);
    }

    public override IQueryable<RemittanceTypeConfiguration> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    private static IQueryable<RemittanceTypeConfiguration> WithIncludes(
        IQueryable<RemittanceTypeConfiguration> query
    )
    {
        return query
            .Include(e => e.RemittanceType)
            .Include(e => e.FieldConfigurations)
                .ThenInclude(e => e.FieldDefinition);
    }
}
