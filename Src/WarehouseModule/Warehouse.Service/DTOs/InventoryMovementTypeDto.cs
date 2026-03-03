namespace NGErp.Warehouse.Service.DTOs;

public record InventoryMovementTypeDto(
    Guid Id,
    string Code,
    string Title,
    bool IncreaseStockQuantity
);

public class CreateInventoryMovementTypeDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public bool IncreaseStockQuantity { get; set; }
}

public class PatchInventoryMovementTypeDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IncreaseStockQuantity { get; set; }
}
