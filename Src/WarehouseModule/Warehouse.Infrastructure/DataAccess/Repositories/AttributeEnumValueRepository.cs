using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeEnumValueRepository(MainDbContext context) :
    Repository<AttributeEnumValue>(context),
    IAttributeEnumValueRepository
{
    public IQueryable<AttributeEnumValue> GetFiltered(
        Guid attributeId,
        RequestParameters requestParameters,
        RequestAdvancedFilters requestAdvancedFilters,
        CancellationToken ct
    )
    {
        var query = base.GetFiltered(requestAdvancedFilters)
            .Where(e => e.AttributeId == attributeId)
            .Include(i => i.Attribute);

        return query;
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
