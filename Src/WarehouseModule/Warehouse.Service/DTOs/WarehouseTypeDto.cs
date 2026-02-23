namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseTypeDto(
    Guid Id,
    string Code,
    string Title,
    bool IsActive
);

public class CreateWarehouseTypeDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public bool IsActive { get; set; } = false;
}

public class PatchWarehouseTypeDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool IsActive { get; set; }
}
