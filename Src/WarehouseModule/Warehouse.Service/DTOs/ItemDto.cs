namespace NGErp.Warehouse.Service.DTOs;

public record ItemDto(
    Guid Id,
    string Code,
    string Title,
    string TitleInEnglish,
    string TechnicalNumber,
    string Sku,
    string Barcode,
    UnitOfMeasurementDto PrimaryUnitOfMeasurement,
    ItemTypeDto ItemType,
    CategoryDto Category,
    List<AttributeSlimDto> ItemAttributes,
    bool IsActive
);

public record ItemListDto(
    Guid Id,
    string Code,
    string Title,
    string TitleInEnglish,
    string TechnicalNumber,
    string Sku,
    string Barcode,
    string PrimaryUnitOfMeasurementTitle,
    string ItemTypeTitle,
    string CategoryTitle,
    bool IsActive
);

public class CreateItemDto
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public string TitleInEnglish { get; set; } = default!;
    public string TechnicalNumber { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public Guid PrimaryUnitOfMeasurementId { get; set; }
    public Guid ItemTypeId { get; set; }
    public Guid CategoryId { get; set; }

    public List<Guid> SecondaryUnitOfMeasurementIds { get; set; } = [];
    public List<Guid> AttributeIds { get; set; } = [];
    public List<CreateItemWarehouseDto> Warehouses { get; set; } = [];
    public List<CreateItemUnitOfMeasurementConversionDto> UnitOfMeasurementConversions { get; set; } = [];
}

public class PatchItemDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? TitleInEnglish { get; set; }
    public string? TechnicalNumber { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public bool? IsActive { get; set; }
    public Guid? PrimaryUnitOfMeasurementId { get; set; }
    public Guid? ItemTypeId { get; set; }
}
