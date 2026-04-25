using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeEnumValueRepository(MainDbContext context) :
    Repository<AttributeEnumValue>(context),
    IAttributeEnumValueRepository
{
    public override async Task<AttributeEnumValue?> SingleOrDefaultAsync(
        Expression<Func<AttributeEnumValue, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return await query
            .Include(e => e.Attribute)
            .SingleOrDefaultAsync(predicate, ct);
    }

    public async Task<int> GetNextCodeAsync(
        Guid attributeId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.AttributeId == attributeId)
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}
