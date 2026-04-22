using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IAttributeEnumValueRepository : 
    IRepository<AttributeEnumValue>
{
    Task<int> GetNextCodeAsync(
        Guid attributeId,
        CancellationToken ct
    );
}
