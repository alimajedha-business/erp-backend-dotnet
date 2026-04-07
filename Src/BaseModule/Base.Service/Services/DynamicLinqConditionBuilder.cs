using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.Json;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.Services;

public static class DynamicLinqConditionBuilder
{
    public static (string where, object[] parameters) Build<TEntity>(
        FilterNodeDto? root,
        IFilterSchemaProvider schemaProvider)
    {
        if (root is null)
            return ("", Array.Empty<object>());

        var schema = schemaProvider.GetSchema<TEntity>();

        var sb = new StringBuilder();
        var prms = new List<object>();

        BuildNode(root, schema, sb, prms);

        return (sb.ToString(), prms.ToArray());
    }

    private static void BuildNode(
        FilterNodeDto node,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms)
    {
        switch (node.Type?.Trim().ToLowerInvariant())
        {
            case "group":
                BuildGroup(node, schema, sb, prms);
                break;

            case "condition":
                BuildCondition(node, schema, sb, prms);
                break;

            default:
                throw new ArgumentException($"Unknown filter node type '{node.Type}'.");
        }
    }

    private static void BuildGroup(
        FilterNodeDto group,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms)
    {
        if (group.Children is null || group.Children.Count == 0)
            throw new ArgumentException("Filter group must have at least one child.");

        var op = NormalizeLogicalOperator(group.Op);
        if (op is null)
            throw new ArgumentException($"Invalid group operator '{group.Op}'. Allowed values are 'and' and 'or'.");

        sb.Append('(');

        for (var i = 0; i < group.Children.Count; i++)
        {
            if (i > 0)
                sb.Append(op == "and" ? " and " : " or ");

            BuildNode(group.Children[i], schema, sb, prms);
        }

        sb.Append(')');
    }

    private static void BuildCondition(
        FilterNodeDto condition,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms)
    {
        if (string.IsNullOrWhiteSpace(condition.Field))
            throw new ArgumentException("Condition.Field is required.");

        if (string.IsNullOrWhiteSpace(condition.Operator))
            throw new ArgumentException("Condition.Operator is required.");

        if (!schema.Fields.TryGetValue(condition.Field, out var fieldInfo))
            throw new ArgumentException($"Field '{condition.Field}' is not allowed.");

        var rawOperator = condition.Operator.Trim();
        if (!fieldInfo.AllowedOps.Contains(rawOperator))
            throw new ArgumentException($"Operator '{rawOperator}' is not allowed for field '{condition.Field}'.");

        var normalizedOp = NormalizeOperator(rawOperator);
        var paramIndex = prms.Count;
        var propertyName = fieldInfo.PropertyName;
        var propertyType = fieldInfo.PropertyType;
        var nonNullableType = GetNonNullableType(propertyType);
        var isNullable = IsNullableType(propertyType);

        if (normalizedOp is "in" or "notin")
        {
            var typedArray = ConvertList(condition.Value, nonNullableType);
            prms.Add(typedArray);

            if (normalizedOp == "in")
            {
                if (isNullable && nonNullableType != typeof(string))
                    sb.Append($"{propertyName} != null && @{paramIndex}.Contains({propertyName}.Value)");
                else
                    sb.Append($"@{paramIndex}.Contains({propertyName})");
            }
            else
            {
                if (isNullable && nonNullableType != typeof(string))
                    sb.Append($"{propertyName} == null || !@{paramIndex}.Contains({propertyName}.Value)");
                else
                    sb.Append($"!@{paramIndex}.Contains({propertyName})");
            }

            return;
        }

        var typedValue = ConvertValue(condition.Value, propertyType);

        if (typedValue is null)
        {
            if (normalizedOp == "eq")
            {
                sb.Append($"{propertyName} == null");
                return;
            }

            if (normalizedOp == "ne")
            {
                sb.Append($"{propertyName} != null");
                return;
            }

            throw new ArgumentException("Only 'eq' and 'ne' are allowed with null values.");
        }

        prms.Add(typedValue);

        switch (normalizedOp)
        {
            case "eq":
                sb.Append($"{propertyName} == @{paramIndex}");
                break;

            case "ne":
                sb.Append($"{propertyName} != @{paramIndex}");
                break;

            case "gt":
                sb.Append($"{propertyName} > @{paramIndex}");
                break;

            case "ge":
                sb.Append($"{propertyName} >= @{paramIndex}");
                break;

            case "lt":
                sb.Append($"{propertyName} < @{paramIndex}");
                break;

            case "le":
                sb.Append($"{propertyName} <= @{paramIndex}");
                break;

            case "startswith":
                EnsureString(fieldInfo);
                sb.Append($"{propertyName} != null && {propertyName}.StartsWith(@{paramIndex})");
                break;

            case "contains":
                EnsureString(fieldInfo);
                sb.Append($"{propertyName} != null && {propertyName}.Contains(@{paramIndex})");
                break;

            case "endswith":
                EnsureString(fieldInfo);
                sb.Append($"{propertyName} != null && {propertyName}.EndsWith(@{paramIndex})");
                break;

            default:
                throw new ArgumentException($"Unsupported operator '{rawOperator}'.");
        }
    }

    private static string? NormalizeLogicalOperator(string? value)
    {
        var normalized = value?.Trim().ToLowerInvariant();
        return normalized is "and" or "or" ? normalized : null;
    }

    private static string NormalizeOperator(string value) =>
        value.Trim().ToLowerInvariant().Replace(" ", "").Replace("_", "");

    private static void EnsureString(FilterFieldInfo info)
    {
        if (GetNonNullableType(info.PropertyType) != typeof(string))
            throw new ArgumentException($"Operator is only valid for strings: '{info.PropertyName}'.");
    }

    private static Type GetNonNullableType(Type type) =>
        Nullable.GetUnderlyingType(type) ?? type;

    private static bool IsNullableType(Type type) =>
        !type.IsValueType || Nullable.GetUnderlyingType(type) is not null;

    private static object? ConvertValue(object? raw, Type targetType)
    {
        if (raw is JsonElement json)
            raw = ConvertJsonElement(json);

        if (raw is null)
            return null;

        var nonNullableType = GetNonNullableType(targetType);

        if (raw is string s && string.IsNullOrWhiteSpace(s))
        {
            if (nonNullableType != typeof(string))
                return null;

            return s;
        }

        if (nonNullableType == typeof(string))
            return raw.ToString();

        if (nonNullableType == typeof(Guid))
        {
            if (raw is Guid guid)
                return guid;

            if (raw is string guidText && Guid.TryParse(guidText, out var parsedGuid))
                return parsedGuid;

            throw new ArgumentException($"Invalid GUID value '{raw}'.");
        }

        if (nonNullableType == typeof(DateTime))
        {
            if (raw is DateTime dt)
                return dt;

            if (raw is DateTimeOffset dto)
                return dto.DateTime;

            if (raw is string dtText &&
                DateTime.TryParse(
                    dtText,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces,
                    out var parsedDateTime))
            {
                return parsedDateTime;
            }

            throw new ArgumentException($"Invalid DateTime value '{raw}'.");
        }

        if (nonNullableType == typeof(DateTimeOffset))
        {
            if (raw is DateTimeOffset dto)
                return dto;

            if (raw is DateTime dt)
                return new DateTimeOffset(dt);

            if (raw is string dtoText &&
                DateTimeOffset.TryParse(
                    dtoText,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces,
                    out var parsedDateTimeOffset))
            {
                return parsedDateTimeOffset;
            }

            throw new ArgumentException($"Invalid DateTimeOffset value '{raw}'.");
        }

        if (nonNullableType == typeof(DateOnly))
        {
            if (raw is DateOnly dateOnly)
                return dateOnly;

            if (raw is DateTime dt)
                return DateOnly.FromDateTime(dt);

            if (raw is string dateOnlyText)
            {
                if (DateOnly.TryParse(
                        dateOnlyText,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AllowWhiteSpaces,
                        out var parsedDateOnly))
                {
                    return parsedDateOnly;
                }

                if (DateTime.TryParse(
                        dateOnlyText,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces,
                        out var parsedDateTimeForDateOnly))
                {
                    return DateOnly.FromDateTime(parsedDateTimeForDateOnly);
                }
            }

            throw new ArgumentException($"Invalid DateOnly value '{raw}'.");
        }

        if (nonNullableType == typeof(TimeOnly))
        {
            if (raw is TimeOnly timeOnly)
                return timeOnly;

            if (raw is DateTime dt)
                return TimeOnly.FromDateTime(dt);

            if (raw is string timeOnlyText)
            {
                if (TimeOnly.TryParse(
                        timeOnlyText,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AllowWhiteSpaces,
                        out var parsedTimeOnly))
                {
                    return parsedTimeOnly;
                }

                if (DateTime.TryParse(
                        timeOnlyText,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces,
                        out var parsedDateTimeForTimeOnly))
                {
                    return TimeOnly.FromDateTime(parsedDateTimeForTimeOnly);
                }
            }

            throw new ArgumentException($"Invalid TimeOnly value '{raw}'.");
        }

        if (nonNullableType.IsEnum)
        {
            if (raw is string enumText)
            {
                if (Enum.TryParse(nonNullableType, enumText, ignoreCase: true, out var enumValue))
                    return enumValue;

                throw new ArgumentException($"Invalid value '{raw}' for enum type '{nonNullableType.Name}'.");
            }

            try
            {
                var numericValue = Convert.ToInt32(raw, CultureInfo.InvariantCulture);
                return Enum.ToObject(nonNullableType, numericValue);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid value '{raw}' for enum type '{nonNullableType.Name}'.");
            }
        }

        if (nonNullableType == typeof(bool))
        {
            if (raw is bool b)
                return b;

            if (raw is string boolText && bool.TryParse(boolText, out var parsedBool))
                return parsedBool;

            throw new ArgumentException($"Invalid boolean value '{raw}'.");
        }

        if (nonNullableType == typeof(decimal))
        {
            if (raw is decimal d)
                return d;

            if (raw is string decimalText &&
                decimal.TryParse(decimalText, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsedDecimal))
            {
                return parsedDecimal;
            }
        }

        if (nonNullableType == typeof(double))
        {
            if (raw is double dbl)
                return dbl;

            if (raw is string doubleText &&
                double.TryParse(doubleText, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var parsedDouble))
            {
                return parsedDouble;
            }
        }

        if (nonNullableType == typeof(float))
        {
            if (raw is float fl)
                return fl;

            if (raw is string floatText &&
                float.TryParse(floatText, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var parsedFloat))
            {
                return parsedFloat;
            }
        }

        if (nonNullableType == typeof(long))
        {
            if (raw is long l)
                return l;

            if (raw is string longText &&
                long.TryParse(longText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedLong))
            {
                return parsedLong;
            }
        }

        if (nonNullableType == typeof(int))
        {
            if (raw is int i)
                return i;

            if (raw is string intText &&
                int.TryParse(intText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedInt))
            {
                return parsedInt;
            }
        }

        if (nonNullableType == typeof(short))
        {
            if (raw is short s16)
                return s16;

            if (raw is string shortText &&
                short.TryParse(shortText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedShort))
            {
                return parsedShort;
            }
        }

        if (nonNullableType == typeof(byte))
        {
            if (raw is byte b8)
                return b8;

            if (raw is string byteText &&
                byte.TryParse(byteText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedByte))
            {
                return parsedByte;
            }
        }

        if (nonNullableType.IsInstanceOfType(raw))
            return raw;

        try
        {
            return Convert.ChangeType(raw, nonNullableType, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Value '{raw}' cannot be converted to type '{nonNullableType.Name}'.");
        }
    }

    private static Array ConvertList(object? raw, Type elementType)
    {
        if (raw is JsonElement json)
        {
            if (json.ValueKind == JsonValueKind.Null)
                return Array.CreateInstance(elementType, 0);

            if (json.ValueKind != JsonValueKind.Array)
                throw new ArgumentException("For 'in'/'notin', value must be a JSON array.");

            var count = json.GetArrayLength();
            var array = Array.CreateInstance(elementType, count);

            var index = 0;
            foreach (var item in json.EnumerateArray())
            {
                var converted = ConvertValue(item, elementType)
                    ?? throw new ArgumentException("Null elements are not supported in 'in'/'notin' arrays.");

                array.SetValue(converted, index++);
            }

            return array;
        }

        if (raw is IEnumerable enumerable && raw is not string)
        {
            var values = new List<object?>();

            foreach (var item in enumerable)
            {
                var converted = ConvertValue(item, elementType)
                    ?? throw new ArgumentException("Null elements are not supported in 'in'/'notin' arrays.");

                values.Add(converted);
            }

            var array = Array.CreateInstance(elementType, values.Count);
            for (var i = 0; i < values.Count; i++)
                array.SetValue(values[i], i);

            return array;
        }

        throw new ArgumentException("For 'in'/'notin', value must be a JSON array or enumerable.");
    }

    private static object? ConvertJsonElement(JsonElement json)
    {
        return json.ValueKind switch
        {
            JsonValueKind.Null => null,
            JsonValueKind.Undefined => null,
            JsonValueKind.String => json.GetString(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Number => ConvertJsonNumber(json),
            JsonValueKind.Array => json,
            JsonValueKind.Object => json,
            _ => json.ToString()
        };
    }

    private static object ConvertJsonNumber(JsonElement json)
    {
        if (json.TryGetInt32(out var i))
            return i;

        if (json.TryGetInt64(out var l))
            return l;

        if (json.TryGetDecimal(out var d))
            return d;

        if (json.TryGetDouble(out var dbl))
            return dbl;

        return json.GetRawText();
    }
}