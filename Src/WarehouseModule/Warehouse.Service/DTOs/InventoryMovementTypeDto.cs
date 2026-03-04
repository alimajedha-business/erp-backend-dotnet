namespace NGErp.Warehouse.Service.DTOs;

public record InventoryMovementTypeDto(
    Guid Id,
    string Code,
    string Title
);

public class CreateInventoryMovementTypeDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
}

public class PatchInventoryMovementTypeDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
}
