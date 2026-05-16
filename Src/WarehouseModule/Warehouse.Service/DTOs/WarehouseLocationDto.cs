namespace NGErp.Warehouse.Service.DTOs;

public record WarehouseLocationDto(
    Guid Id,
    int Code,
    string Title,
    bool CanStoreItem,
    bool HasNextLevel,
    int LevelNo,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? MaxMass,
    decimal? MaxVolume,
    SiUnitAsReferenceDto? PreferredMassUnit,
    SiUnitAsReferenceDto? PreferredLengthUnit,
    SiUnitAsReferenceDto? PreferredVolumeUnit,
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
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? MaxMass,
    decimal? MaxVolume,
    SiUnitAsReferenceDto? PreferredMassUnit,
    SiUnitAsReferenceDto? PreferredLengthUnit,
    SiUnitAsReferenceDto? PreferredVolumeUnit,
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
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? MaxMass { get; set; }
    public decimal? MaxVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
}

public class PatchWarehouseLocationDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? CanStoreItem { get; set; }
    public bool? HasNextLevel { get; set; }
    public int? LevelNo { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? MaxMass { get; set; }
    public decimal? MaxVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
}

public sealed record WarehouseLocationNode(
    Guid Id,
    int Code,
    string Title,
    Guid? ParentLocationId,
    Guid WarehouseId
);
