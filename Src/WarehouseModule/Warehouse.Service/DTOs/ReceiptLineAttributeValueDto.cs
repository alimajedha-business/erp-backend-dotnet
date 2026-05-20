namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineAttributeValueDto(
    Guid Id,
    Guid ItemAttributeId,
    string? StringValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
    Guid? ReferenceId,
    bool? BooleanValue
);

public class CreateReceiptLineAttributeValueDto
{
    public Guid ItemAttributeId { get; set; }

    public string? StringValue { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }
}
