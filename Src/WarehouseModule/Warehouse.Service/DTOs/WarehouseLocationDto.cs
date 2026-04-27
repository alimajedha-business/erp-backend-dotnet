namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseLocationDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem,
    bool HasNextLevel,
    int LevelNo,
    WarehouseSlimDto Warehouse
);

public record WarehouseLocationSlimDto(
    Guid Id,
    int Code,
    string Title
);

public record WarehouseLocationListDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem,
    bool HasNextLevel,
    int LevelNo,
    string WarehouseTitle
);

public class CreateWarehouseLocationDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public Guid? ParentLocationId { get; set; }
    public bool CanStoreItem { get; set; } = true;
    public bool HasNextLevel { get; set; } = false;
    public int LevelNo { get; set; }
}

public class PatchWarehouseLocationDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? CanStoreItem { get; set; }
    public bool? HasNextLevel { get; set; }
}

public sealed record WarehouseLocationNode(
    Guid Id,
    int Code,
    string Title,
    Guid? ParentLocationId,
    Guid WarehouseId
);