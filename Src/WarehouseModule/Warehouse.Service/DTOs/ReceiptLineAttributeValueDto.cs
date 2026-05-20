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

    public object? Value { get; set; }
    public string? Type { get; set; }
    public string? DataType { get; set; }

    public string? StringValue { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public void NormalizeValue()
    {
        var effectiveType = Type ?? DataType;

        if (Value is null || string.IsNullOrWhiteSpace(effectiveType))
            return;

        var typedValue = ReceiptTypedValueConverter.Convert(Value, effectiveType);

        StringValue = typedValue.StringValue;
        IntegerValue = typedValue.IntegerValue;
        DecimalValue = typedValue.DecimalValue;
        DateValue = typedValue.DateValue;
        ReferenceId = typedValue.ReferenceId;
        BooleanValue = typedValue.BooleanValue;
    }
}
