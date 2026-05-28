namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptTypeConfigurationDto(
    Guid Id,
    ReceiptTypeSlimDto ReceiptType,
    IReadOnlyList<ReceiptTypeFieldConfigurationDto> FieldConfigurations
);

public record ReceiptTypeConfigurationListDto(
    Guid Id,
    Guid ReceiptTypeId,
    string ReceiptTypeTitle,
    int FieldConfigurationCount
);

public class CreateReceiptTypeConfigurationDto
{
    public Guid ReceiptTypeId { get; set; }
    public List<CreateReceiptTypeFieldConfigurationDto>? FieldConfigurations { get; set; }
}
