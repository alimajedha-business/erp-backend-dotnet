namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseTypeDto(
    Guid Id,
    int Code,
    string Title,
    bool IsActive
);

public class CreateWarehouseTypeDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required bool IsActive { get; set; } = true;
}

public class PatchWarehouseTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsActive { get; set; }
}
