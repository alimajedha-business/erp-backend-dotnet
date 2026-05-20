using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceTypeFieldConfigurationDto(
    Guid Id,
    Guid RemittanceTypeConfigurationId,
    Guid RemittanceTypeId,
    Guid FieldDefinitionId,
    bool Exists,
    bool IsRequired,
    ReceiptConfiguredPlacement Placement,
    string PlacementDescription,
    ReceiptFieldDefinitionDto FieldDefinition
)
{
    public static string GetPlacementDescription(ReceiptConfiguredPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record RemittanceTypeFieldConfigurationListDto(
    Guid Id,
    Guid RemittanceTypeConfigurationId,
    Guid RemittanceTypeId,
    Guid FieldDefinitionId,
    string FieldDefinitionKey,
    string FieldDefinitionTitle,
    bool Exists,
    bool IsRequired,
    ReceiptConfiguredPlacement Placement,
    string PlacementDescription
);

public class CreateRemittanceTypeFieldConfigurationDto
{
    public Guid FieldDefinitionId { get; set; }
    public bool? Exists { get; set; }
    public bool? IsRequired { get; set; }
    public ReceiptConfiguredPlacement? Placement { get; set; }
}

public class PatchRemittanceTypeFieldConfigurationDto
{
    public bool? Exists { get; set; }
    public bool? IsRequired { get; set; }
    public ReceiptConfiguredPlacement? Placement { get; set; }
}
