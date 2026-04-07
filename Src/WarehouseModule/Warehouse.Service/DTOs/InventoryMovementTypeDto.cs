namespace NGErp.Warehouse.Service.DTOs;

public record InventoryMovementTypeDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateInventoryMovementTypeDto
{
    public int Code { get; set; }
    public required string Title { get; set; }
}

public class PatchInventoryMovementTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
}
