using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Specifications;

public class WarehouseLocationSpecification(
    Guid warehouseId
) : ISpecification<WarehouseLocation>
{
    private readonly Guid _warehouseId = warehouseId;

    public Func<
        IQueryable<WarehouseLocation>,
        IQueryable<WarehouseLocation>
    > Query => query => query
        .Where(e => e.WarehouseId == _warehouseId)
        .Include(i => i.Warehouse);
}

public class WarehouseLocationListSpecification(
    Guid warehouseId
) : ISpecification<WarehouseLocation>
{
    private readonly Guid _warehouseId = warehouseId;

    public Func<
        IQueryable<WarehouseLocation>,
        IQueryable<WarehouseLocation>
    > Query => query => query
        .Where(e => e.WarehouseId == _warehouseId)
        .Include(i => i.Warehouse);
}
