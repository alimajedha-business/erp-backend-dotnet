using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ItemWarehouseDto(
    WarehouseSlimDto Warehouse,
    decimal ReorderPoint,
    decimal CriticalPoint,
    decimal ReorderQuantity,
    decimal MaxStockLevel,
    List<WarehouseLocationSlimDto> Locations
);

public class CreateItemWarehouseDto
{
    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal CriticalPoint { get; set; }
    public decimal ReorderQuantity { get; set; }
    public decimal MaxStockLevel { get; set; }
    public List<Guid> LocationIds { get; set; } = [];
}
