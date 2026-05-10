namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptTypeConfigurationDto(
    Guid Id,
    Guid ReceiptTypeId,
    ReceiptTypeSlimDto ReceiptType,
    IReadOnlyList<ReceiptTypeFieldConfigurationListDto> FieldConfigurations
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
