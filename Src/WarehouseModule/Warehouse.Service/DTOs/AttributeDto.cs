using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record AttributeDto(
    Guid Id,
    int Code,
    string Title,
    AttributeDataType DataType,
    string DataTypeDescription,
    AttributeEntity AttributeEntity,
    string AttributeEntityDescription,
    bool IsRequired,
    bool IsStockDimension,
	bool IsStatic
)
{
    public static string GetDataTypeDescription(AttributeDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static string GetEntityDescription(AttributeEntity value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record AttributeSlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateAttributeDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required AttributeDataType DataType { get; set; }
    public required AttributeEntity AttributeEntity { get; set; }
    public bool IsRequired { get; set; } = false;
    public bool IsStockDimension { get; set; } = false;
}

public class PatchAttributeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public AttributeDataType? DataType { get; set; }
    public AttributeEntity? AttributeEntity { get; set; }
}