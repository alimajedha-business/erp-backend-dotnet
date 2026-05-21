using System.Globalization;

namespace NGErp.Warehouse.Service.DTOs;

internal static class ReceiptTypedValueConverter
{
    public static ReceiptTypedValue Convert(object? value, string type)
    {
        var normalizedType = type.Trim().ToLowerInvariant();

        if (value is null)
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

    private static string? GetString(object value)
    {
        return value.ToString();
    }

    private static int GetInteger(object value)
    {
        return value switch
        {
            int integer => integer,
            long longValue => checked((int)longValue),
            _ => int.Parse(NormalizeNumber(value), NumberStyles.Integer, CultureInfo.InvariantCulture)
        };
    }

    private static decimal GetDecimal(object value)
    {
        return value switch
        {
            decimal decimalValue => decimalValue,
            double doubleValue => ConvertToDecimal(doubleValue),
            float floatValue => ConvertToDecimal(floatValue),
            int integer => integer,
            long longValue => longValue,
            _ => decimal.Parse(NormalizeNumber(value), NumberStyles.Number, CultureInfo.InvariantCulture)
        };
    }

    private static DateOnly GetDate(object value)
    {
        return DateOnly.Parse(value.ToString()!, CultureInfo.InvariantCulture);
    }

    private static bool GetBoolean(object value)
    {
        return value switch
        {
            bool booleanValue => booleanValue,
            _ => bool.Parse(value.ToString()!)
        };
    }

    private static Guid GetGuid(object value)
    {
        return Guid.Parse(value.ToString()!);
    }

    private static decimal ConvertToDecimal(double value)
    {
        return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    private static decimal ConvertToDecimal(float value)
    {
        return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    private static string NormalizeNumber(object value)
    {
        return value
            .ToString()!
            .Trim()
            .Replace('٫', '.')
            .Replace('٬', ',')
            .Replace('−', '-');
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
