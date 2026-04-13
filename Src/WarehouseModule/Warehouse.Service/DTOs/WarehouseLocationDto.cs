namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseLocationDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem,
    int LevelNo,
    WarehouseSlimDto Warehouse
);

public record WarehouseLocationListDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem,
    int LevelNo,
    string WarehouseTitle
);

public class CreateWarehouseLocationDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public Guid? ParentLocationId { get; set; }
    public bool CanStoreItem { get; set; } = true;
    public int LevelNo { get; set; }
}

public class PatchWarehouseLocationDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? CanStoreItem { get; set; }
}