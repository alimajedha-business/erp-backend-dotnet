using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record AttributeDto(
    Guid Id,
    string Code,
    string Title,
    AttributeDataType DataType,
    bool IsItemAttribute,
    bool IsRequired,
    bool IsStockDimension
);

public class CreateAttributeDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public AttributeDataType DataType { get; set; }
    public bool IsItemAttribute { get; set; } = false;
    public bool IsRequired { get; set; } = false;
    public bool IsStockDimension { get; set; } = false;
}

public class PatchAttributeDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
}