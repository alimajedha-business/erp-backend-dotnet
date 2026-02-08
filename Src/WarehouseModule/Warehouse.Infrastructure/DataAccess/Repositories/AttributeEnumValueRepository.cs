using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeEnumValueRepository(MainDbContext context) :
    RepositoryWithCompany<AttributeEnumValue>(context),
    IAttributeEnumValueRepository
{
    public async Task<ListQueryResult<AttributeEnumValue>> GetAllAsync(
        Guid companyId,
        Guid attributeId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var query = _context
            .Set<AttributeEnumValue>()
            .AsNoTracking()
            .Where(aev => aev.CompanyId == companyId && aev.AttributeId == attributeId);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<AttributeEnumValue>(items, totalCount);
    }
}
