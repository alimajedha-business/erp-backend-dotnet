using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IAttributeEnumValueRepository : 
    IRepository<AttributeEnumValue>
{
    IQueryable<AttributeEnumValue> GetFiltered(
        Guid attributeId,
        RequestParameters requestParameters,
        RequestAdvancedFilters requestAdvancedFilters,
        CancellationToken ct = default
    );

    Task<int> GetNextCodeAsync(
        Guid attributeId,
        CancellationToken ct
    );
}
