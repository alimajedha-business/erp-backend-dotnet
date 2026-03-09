using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record AttributeDto(
    Guid Id,
    string Code,
    string Title,
    AttributeDataType DataType,
    string DataTypeDescription,
    bool IsItemAttribute,
    bool IsRequired,
    bool IsStockDimension
)
{
    public static string GetDescription(AttributeDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record AttributeSlimDto(
    Guid Id,
    string Code,
    string Title
);

public class CreateAttributeDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public required AttributeDataType DataType { get; set; }
    public bool IsItemAttribute { get; set; } = false;
    public bool IsRequired { get; set; } = false;
    public bool IsStockDimension { get; set; } = false;
}

public class PatchAttributeDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public AttributeDataType? DataType { get; set; }
}