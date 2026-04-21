using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IAttributeEnumValueRepository : 
    IRepository<AttributeEnumValue>
{
    Task<ListQueryResult<AttributeEnumValue>> GetAllAsync(
        Guid attributeId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<int> GetNextCodeAsync(
        Guid attributeId,
        CancellationToken ct
    );
}
