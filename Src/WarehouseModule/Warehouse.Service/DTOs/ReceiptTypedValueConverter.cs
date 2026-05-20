using System.Globalization;
using System.Text.Json;

namespace NGErp.Warehouse.Service.DTOs;

internal static class ReceiptTypedValueConverter
{
    public static ReceiptTypedValue Convert(JsonElement value, string type)
    {
        var normalizedType = type.Trim().ToLowerInvariant();

        if (value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
            return ReceiptTypedValue.Empty;

        return normalizedType switch
        {
            "string" => ReceiptTypedValue.FromString(GetString(value)),
            "integer" or "int" => ReceiptTypedValue.FromInteger(GetInteger(value)),
            "decimal" or "number" => ReceiptTypedValue.FromDecimal(GetDecimal(value)),
            "date" => ReceiptTypedValue.FromDate(GetDate(value)),
            "boolean" or "bool" => ReceiptTypedValue.FromBoolean(GetBoolean(value)),
            "guid" or "reference" or "referenceid" => ReceiptTypedValue.FromReference(GetGuid(value)),
            _ => ReceiptTypedValue.Empty
        };
    }

    private static string? GetString(JsonElement value)
    {
        return value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : value.ToString();
    }

    private static int GetInteger(JsonElement value)
    {
        return value.ValueKind == JsonValueKind.Number
            ? value.GetInt32()
            : int.Parse(value.GetString()!, CultureInfo.InvariantCulture);
    }

    private static decimal GetDecimal(JsonElement value)
    {
        return value.ValueKind == JsonValueKind.Number
            ? value.GetDecimal()
            : decimal.Parse(value.GetString()!, CultureInfo.InvariantCulture);
    }

    private static DateOnly GetDate(JsonElement value)
    {
        return DateOnly.Parse(value.GetString()!, CultureInfo.InvariantCulture);
    }

    private static bool GetBoolean(JsonElement value)
    {
        return value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => bool.Parse(value.GetString()!)
        };
    }

    private static Guid GetGuid(JsonElement value)
    {
        return Guid.Parse(value.GetString()!);
    }
}

internal readonly record struct ReceiptTypedValue(
    string? StringValue,
    int? IntegerValue,
    decimal? DecimalValue,
    DateOnly? DateValue,
    Guid? ReferenceId,
    bool? BooleanValue
)
{
    public static ReceiptTypedValue Empty => new(null, null, null, null, null, null);

    public static ReceiptTypedValue FromString(string? value) =>
        new(value, null, null, null, null, null);

    public static ReceiptTypedValue FromInteger(int value) =>
        new(null, value, null, null, null, null);

    public static ReceiptTypedValue FromDecimal(decimal value) =>
        new(null, null, value, null, null, null);

    public static ReceiptTypedValue FromDate(DateOnly value) =>
        new(null, null, null, value, null, null);

    public static ReceiptTypedValue FromReference(Guid value) =>
        new(null, null, null, null, value, null);

    public static ReceiptTypedValue FromBoolean(bool value) =>
        new(null, null, null, null, null, value);
}
