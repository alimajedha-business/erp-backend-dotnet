namespace NGErp.Warehouse.Service.DTOs;

public record InventoryMovementTypeDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateInventoryMovementTypeDto
{
    public required int Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
}

public class PatchInventoryMovementTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
}
