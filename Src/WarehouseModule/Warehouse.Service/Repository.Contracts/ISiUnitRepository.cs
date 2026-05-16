using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ISiUnitRepository : IRepository<SiUnit>
{
    Task<IReadOnlyDictionary<Guid, SiUnit>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken ct
    );

    Task<IReadOnlyDictionary<UnitDimension, SiUnit>> GetBaseUnitsByDimensionsAsync(
        IEnumerable<UnitDimension> dimensions,
        CancellationToken ct
    );
}
