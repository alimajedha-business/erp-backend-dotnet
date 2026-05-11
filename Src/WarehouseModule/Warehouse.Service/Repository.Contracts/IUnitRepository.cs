using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IUnitRepository : IRepository<Unit>
{
    Task<IReadOnlyDictionary<Guid, Unit>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken ct
    );

    Task<IReadOnlyDictionary<UnitDimension, Unit>> GetBaseUnitsByDimensionsAsync(
        IEnumerable<UnitDimension> dimensions,
        CancellationToken ct
    );
}
