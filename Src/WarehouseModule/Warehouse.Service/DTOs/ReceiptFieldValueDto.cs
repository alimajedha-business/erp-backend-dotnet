using System.Text.Json;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptFieldValueDto(
    Guid Id,
    Guid FieldDefinitionId,
    Guid? ReceiptLineId,
    string? StringValue,
    int? IntegerValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
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

    public JsonElement? Value { get; set; }
    public string? Type { get; set; }

    public string? StringValue { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public void NormalizeValue()
    {
        if (Value is null || string.IsNullOrWhiteSpace(Type))
            return;

        var typedValue = ReceiptTypedValueConverter.Convert(Value.Value, Type);

        StringValue = typedValue.StringValue;
        IntegerValue = typedValue.IntegerValue;
        DecimalValue = typedValue.DecimalValue;
        DateValue = typedValue.DateValue;
        ReferenceId = typedValue.ReferenceId;
        BooleanValue = typedValue.BooleanValue;
    }
}
