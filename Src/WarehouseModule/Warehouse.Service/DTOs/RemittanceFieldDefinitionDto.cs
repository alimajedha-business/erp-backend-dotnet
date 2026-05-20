using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceFieldDefinitionDto(
    Guid Id,
    string Key,
    string Title,
    RemittanceFieldDataType DataType,
    string DataTypeDescription,
    RemittanceFieldPlacement AllowedPlacement,
    string AllowedPlacementDescription,
    bool IsActive
)
{
    public static string GetDataTypeDescription(RemittanceFieldDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static string GetPlacementDescription(RemittanceFieldPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record RemittanceFieldDefinitionListDto(
    Guid Id,
    string Key,
    string Title,
    RemittanceFieldPlacement AllowedPlacement
);
