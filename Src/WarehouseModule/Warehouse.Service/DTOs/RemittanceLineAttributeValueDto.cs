namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceLineAttributeValueDto(
    Guid Id,
    Guid ItemAttributeId,
    string? StringValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
    DateTime? DateTimeValue,
    Guid? ReferenceId,
    bool? BooleanValue
);

public class CreateRemittanceLineAttributeValueDto
{
    public Guid ItemAttributeId { get; set; }
    public string? StringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }
}
