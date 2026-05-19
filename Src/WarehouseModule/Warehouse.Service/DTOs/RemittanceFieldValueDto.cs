namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceFieldValueDto(
    Guid Id,
    Guid FieldDefinitionId,
    Guid? RemittanceLineId,
    string? StringValue,
    int? IntValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
    DateTime? DateTimeValue,
    Guid? ReferenceId,
    bool? BooleanValue
);

public class CreateRemittanceFieldValueDto
{
    public Guid FieldDefinitionId { get; set; }
    public string? StringValue { get; set; }
    public int? IntValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }
}
