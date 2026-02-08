using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IAttributeEnumValueRepository : 
    IRepositoryWithCompany<AttributeEnumValue>
{
    Task<ListQueryResult<AttributeEnumValue>> GetAllAsync(
        Guid companyId,
        Guid attributeId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
}
