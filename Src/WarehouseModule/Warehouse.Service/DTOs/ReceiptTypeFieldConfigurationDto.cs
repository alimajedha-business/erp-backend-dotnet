using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptTypeFieldConfigurationDto(
    Guid Id,
    Guid FieldDefinitionId,
    string FieldDefinitionKey,
    string FieldDefinitionTitle,
    bool Exists,
    bool IsRequired,
    ReceiptConfiguredPlacement Placement,
    string PlacementDescription,
    ReceiptFieldDataType FieldDataType,
    string FieldDataTypeDescription,
    ReceiptReferenceEntityType ReferenceEntityType
)
{
    public static string GetDataTypeDescription(ReceiptFieldDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static string GetPlacementDescription(ReceiptConfiguredPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public class CreateReceiptTypeFieldConfigurationDto
{
    public Guid FieldDefinitionId { get; set; }
    public bool? Exists { get; set; }
    public bool? IsRequired { get; set; }
    public ReceiptConfiguredPlacement? Placement { get; set; }
}

public class PatchReceiptTypeFieldConfigurationDto
{
    public bool? Exists { get; set; }
    public bool? IsRequired { get; set; }
    public ReceiptConfiguredPlacement? Placement { get; set; }
}
