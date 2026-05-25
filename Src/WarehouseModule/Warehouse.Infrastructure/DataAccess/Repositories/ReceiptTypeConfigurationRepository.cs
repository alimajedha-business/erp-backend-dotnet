using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
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
        return query
            .Include(e => e.ReceiptType)
            .Include(e => e.FieldConfigurations).ThenInclude(e => e.FieldDefinition)
            .SingleOrDefaultAsync(predicate, ct);
    }
}
