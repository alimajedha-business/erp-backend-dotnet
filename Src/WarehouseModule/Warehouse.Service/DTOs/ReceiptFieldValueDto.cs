using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptFieldValueDto(
    Guid Id,
    Guid FieldDefinitionId,
    Guid? ReceiptLineId,
    string? StringValue,
    int? IntValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
    DateTime? DateTimeValue,
    Guid? ReferenceId,
    bool? BooleanValue
);

public record ReceiptHeaderFieldValueListDto(
    Guid Id,
    Guid FieldDefinitionId,
    string FieldDefinitionTitle,
    string FieldDefinitionKey,
    ReceiptFieldDataType DataType,
    string DataTypeDescription,
    object? Value
);

public class CreateReceiptFieldValueDto
{
    public Guid FieldDefinitionId { get; set; }

    public object? Value { get; set; }
    public string? Type { get; set; }

    public string? StringValue { get; set; }
    public int? IntValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }
}
