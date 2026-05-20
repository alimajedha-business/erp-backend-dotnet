namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceTypeConfigurationDto(
    Guid Id,
    Guid RemittanceTypeId,
    RemittanceTypeSlimDto RemittanceType,
    IReadOnlyList<RemittanceTypeFieldConfigurationListDto> FieldConfigurations
);

public record RemittanceTypeConfigurationListDto(
    Guid Id,
    Guid RemittanceTypeId,
    string RemittanceTypeTitle,
    int FieldConfigurationCount
);

public class CreateRemittanceTypeConfigurationDto
{
    public Guid RemittanceTypeId { get; set; }
    public List<CreateRemittanceTypeFieldConfigurationDto>? FieldConfigurations { get; set; }
}
