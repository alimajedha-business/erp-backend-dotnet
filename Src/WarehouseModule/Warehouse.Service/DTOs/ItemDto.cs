using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace NGErp.Warehouse.Service.DTOs;

public record ItemDto(
    Guid Id,
    string Code,
    string Title,
    string TitleInEnglish,
    string TechnicalNumber,
    string Sku,
    string Barcode,
    decimal? Weight,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? Volume,
    SiUnitAsReferenceDto? PreferredMassUnit,
    SiUnitAsReferenceDto? PreferredLengthUnit,
    SiUnitAsReferenceDto? PreferredVolumeUnit,
    ItemTypeSlimDto ItemType,
    CategorySlimDto Category,
    List<AttributeSlimDto> Attributes,
    List<ItemUnitOfMeasurementDto> ItemUnitOfMeasurements,
    List<ItemWarehouseDto> ItemWarehouses,
    Dictionary<string, ItemUnitConversionEquationDto> UnitConversions,
    bool IsActive
);

public record ItemSlimDto(
    Guid Id,
    string Code,
    string Title,
    string TitleInEnglish,
    string Sku
);

public record ItemListDto(
    Guid Id,
    string Code,
    string Title,
    string TitleInEnglish,
    string TechnicalNumber,
    string Sku,
    string Barcode,
    string UnitOfMeasurementTitle,
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
    public Guid ItemTypeId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Volume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }

    public List<CreateItemUnitOfMeasurementDto> ItemUnitOfMeasurements { get; set; } = [];
    public List<Guid> AttributeIds { get; set; } = [];
    public List<CreateItemWarehouseDto> ItemWarehouses { get; set; } = [];
    public Dictionary<string, ItemUnitConversionEquationDto> ItemUnitOfMeasurementConversions { get; set; } = [];

    [JsonProperty("unitConversions")]
    [JsonPropertyName("unitConversions")]
    public Dictionary<string, ItemUnitConversionEquationDto>? ItemUnitOfMeasurementConversionsAlias
    {
        get => null; // never used for output
        set
        {
            if (value != null)
                ItemUnitOfMeasurementConversions = value;
        }
    }
}

public class PatchItemDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? TitleInEnglish { get; set; }
    public string? TechnicalNumber { get; set; }
    public string? Barcode { get; set; }
    public bool? IsActive { get; set; }
    public Guid? ItemTypeId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Volume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }

    public List<CreateItemUnitOfMeasurementDto>? ItemUnitOfMeasurements { get; set; }
    public List<Guid>? AttributeIds { get; set; }
    public List<CreateItemWarehouseDto>? ItemWarehouses { get; set; }
    public Dictionary<string, ItemUnitConversionEquationDto>? ItemUnitOfMeasurementConversions { get; set; }

    [JsonProperty("itemAttributes")]
    [JsonPropertyName("itemAttributes")]
    public List<Guid>? ItemAttributesAlias
    {
        get => null; // never used for output
        set
        {
            if (value != null)
                AttributeIds = value;
        }
    }

    [JsonProperty("unitConversions")]
    [JsonPropertyName("unitConversions")]
    public Dictionary<string, ItemUnitConversionEquationDto>? ItemUnitOfMeasurementConversionsAlias
    {
        get => null; // never used for output
        set
        {
            if (value != null)
                ItemUnitOfMeasurementConversions = value;
        }
    }
}
