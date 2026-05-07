using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptFieldDefinitionDto(
    Guid Id,
    string Key,
    string Title,
    ReceiptFieldDataType DataType,
    string DataTypeDescription,
    ReceiptFieldPlacement AllowedPlacement,
    string AllowedPlacementDescription,
    bool IsActive
)
{
    public static string GetDataTypeDescription(ReceiptFieldDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static string GetPlacementDescription(ReceiptFieldPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record ReceiptFieldDefinitionSlimDto(
    Guid Id,
    string Key,
    string Title
);
