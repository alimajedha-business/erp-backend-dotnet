using System.Globalization;
using System.Text.Json;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

internal static class ReceiptFieldValueInputNormalizer
{
    public static string? Normalize(CreateReceiptFieldValueDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Type))
            return null;

        if (!HasIncomingValue(dto.Value))
            return null;

        try
        {
            ClearTypedValues(dto);

            switch (NormalizeType(dto.Type))
            {
                case "text":
                case "string":
                    dto.StringValue = GetString(dto.Value);
                    break;
                case "integer":
                case "int":
                    dto.IntValue = GetInt(dto.Value);
                    break;
                case "decimal":
                    dto.DecimalValue = GetDecimal(dto.Value);
                    break;
                case "date":
                    dto.DateValue = GetDate(dto.Value);
                    break;
                case "datetime":
                case "date-time":
                    dto.DateTimeValue = GetDateTime(dto.Value);
                    break;
                case "guid":
                case "reference":
                    dto.ReferenceId = GetGuid(dto.Value);
                    break;
                case "boolean":
                case "bool":
                    dto.BooleanValue = GetBool(dto.Value);
                    break;
                default:
                    return $"Unsupported receipt field value type '{dto.Type}'.";
            }
        }
        catch (Exception ex) when (ex is FormatException or InvalidOperationException or OverflowException)
        {
            return $"Receipt field value '{dto.FieldDefinitionId}' is invalid for type '{dto.Type}'.";
        }

        return null;
    }

    private static string NormalizeType(string type) =>
        type.Trim().ToLowerInvariant();

    private static bool HasIncomingValue(object? value) =>
        value is not null &&
        (value is not JsonElement jsonElement || jsonElement.ValueKind != JsonValueKind.Null);

    private static void ClearTypedValues(CreateReceiptFieldValueDto dto)
    {
        dto.StringValue = null;
        dto.IntValue = null;
        dto.DecimalValue = null;
        dto.DateValue = null;
        dto.DateTimeValue = null;
        dto.ReferenceId = null;
        dto.BooleanValue = null;
    }

    private static string? GetString(object? value)
    {
        if (value is JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.String
                ? jsonElement.GetString()
                : jsonElement.GetRawText();
        }

        return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    private static int GetInt(object? value)
    {
        if (value is JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.Number
                ? jsonElement.GetInt32()
                : int.Parse(jsonElement.GetString()!, CultureInfo.InvariantCulture);
        }

        return Convert.ToInt32(value, CultureInfo.InvariantCulture);
    }

    private static decimal GetDecimal(object? value)
    {
        if (value is JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.Number
                ? jsonElement.GetDecimal()
                : decimal.Parse(jsonElement.GetString()!, CultureInfo.InvariantCulture);
        }

        return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    private static DateOnly GetDate(object? value)
    {
        if (value is DateOnly dateOnly)
            return dateOnly;

        if (value is DateTime dateTime)
            return DateOnly.FromDateTime(dateTime);

        var text = GetString(value);
        return DateOnly.Parse(text!, CultureInfo.InvariantCulture);
    }

    private static DateTime GetDateTime(object? value)
    {
        if (value is DateTime dateTime)
            return dateTime;

        var text = GetString(value);
        return DateTime.Parse(text!, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }

    private static Guid GetGuid(object? value)
    {
        if (value is Guid guid)
            return guid;

        var text = GetString(value);
        return Guid.Parse(text!);
    }

    private static bool GetBool(object? value)
    {
        if (value is JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.String => bool.Parse(jsonElement.GetString()!),
                _ => throw new FormatException()
            };
        }

        return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
    }
}
