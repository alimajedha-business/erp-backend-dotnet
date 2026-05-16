namespace NGErp.Warehouse.Service.Repository.Contracts.Models;

public record WarehouseLocationOccupancy(
    Guid WarehouseLocationId,
    decimal OccupiedMass,
    decimal OccupiedVolume
);
