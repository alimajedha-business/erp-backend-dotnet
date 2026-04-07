namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseLocationDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem
);

public class CreateWarehouseLocationDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public Guid? ParentLocationId { get; set; }
    public Guid WarehouseId { get; set; }
    public bool CanStoreItem { get; set; } = true;
}

public class PatchWarehouseLocationDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? CanStoreItem { get; set; }
}