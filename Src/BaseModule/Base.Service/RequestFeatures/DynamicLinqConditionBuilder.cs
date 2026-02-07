using System.Globalization;
using System.Text;
using System.Text.Json;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.RequestFeatures;

public static class DynamicLinqConditionBuilder
{
    public static (string where, object[] parameters) Build<TEntity>(
        FilterNodeDto root,
        IFilterSchemaProvider _schemaProvider
    )
    {
        var schema = _schemaProvider.GetSchema<TEntity>();

        var sb = new StringBuilder();
        var prms = new List<object>();

        BuildNode(root, schema, sb, prms);

        return (sb.ToString(), prms.ToArray());
    }

    private static void BuildNode(
        FilterNodeDto node,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms
    )
    {
        switch (node)
        {
            case FilterGroupDto g:
                BuildGroup(g, schema, sb, prms);
                break;
            case FilterConditionDto c:
                BuildCondition(c, schema, sb, prms);
                break;
            default:
                throw new ArgumentException("Unknown filter node type.");
        }
    }

    private static void BuildGroup(
        FilterGroupDto g,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms
    )
    {
        if (g.Children is null || g.Children.Count == 0)
            throw new ArgumentException("Filter group must have children.");

        var op = (g.Op ?? "").Trim().ToLowerInvariant();
        if (op is not ("and" or "or"))
            throw new ArgumentException($"Invalid group operator '{g.Op}'.");

        sb.Append('(');
        for (int i = 0; i < g.Children.Count; i++)
        {
            if (i > 0)
                sb.Append(op == "and" ? " and " : " or ");
            BuildNode(g.Children[i], schema, sb, prms);
        }
        sb.Append(')');
    }

    private static void BuildCondition(
        FilterConditionDto c,
        FilterSchema schema,
        StringBuilder sb,
        List<object> prms
    )
    {
        if (string.IsNullOrWhiteSpace(c.Field))
            throw new ArgumentException("Condition.Field is required.");
        if (string.IsNullOrWhiteSpace(c.Operator))
            throw new ArgumentException("Condition.Operator is required.");

        if (!schema.Fields.TryGetValue(c.Field, out var fieldInfo))
            throw new ArgumentException($"Field '{c.Field}' is not allowed.");

        var op = c.Operator.Trim();
        if (!fieldInfo.AllowedOps.Contains(op))
            throw new ArgumentException($"Operator '{op}' is not allowed for field '{c.Field}'.");

        var paramIndex = prms.Count;

        // Normalize operator for switching:
        // - lowercase
        // - remove spaces and underscores (so "not in", "not_in", "notin" all work)
        var normalizedOp = op.Trim().ToLowerInvariant().Replace(" ", "").Replace("_", "");

        // Special handling for set operators:
        // Dynamic LINQ works well with: @0.Contains(Field) / !@0.Contains(Field)
        if (normalizedOp is "in" or "notin")
        {
            var elementType = Nullable.GetUnderlyingType(fieldInfo.PropertyType) ?? fieldInfo.PropertyType;
            var typedList = ConvertList(c.Value, elementType);
            prms.Add(typedList);

            var propName = fieldInfo.PropertyName;
            var isNullable = Nullable.GetUnderlyingType(fieldInfo.PropertyType) is not null;

            if (normalizedOp == "in")
            {
                if (isNullable)
                    sb.Append($"{propName} != null && @{paramIndex}.Contains({propName}.Value)");
                else
                    sb.Append($"@{paramIndex}.Contains({propName})");
            }
            else // notin
            {
                if (isNullable)
                    sb.Append($"{propName} == null || !@{paramIndex}.Contains({propName}.Value)");
                else
                    sb.Append($"!@{paramIndex}.Contains({propName})");
            }

            return;
        }

        var typedValue = ConvertValue(c.Value, fieldInfo.PropertyType);

        if (typedValue is null)
        {
            if (normalizedOp is "eq")
            {
                sb.Append($"{fieldInfo.PropertyName} == null");
                return;
            }
            if (normalizedOp is "ne")
            {
                sb.Append($"{fieldInfo.PropertyName} != null");
                return;
            }
            throw new ArgumentException("Only 'eq' and 'ne' are allowed with null values.");
        }

        prms.Add(typedValue);

        switch (normalizedOp)
        {
            case "eq":
                sb.Append($"{fieldInfo.PropertyName} == @{paramIndex}");
                break;

            case "ne":
                sb.Append($"{fieldInfo.PropertyName} != @{paramIndex}");
                break;

            case "gt":
                sb.Append($"{fieldInfo.PropertyName} > @{paramIndex}");
                break;

            case "ge":
                sb.Append($"{fieldInfo.PropertyName} >= @{paramIndex}");
                break;

            case "lt":
                sb.Append($"{fieldInfo.PropertyName} < @{paramIndex}");
                break;

            case "le":
                sb.Append($"{fieldInfo.PropertyName} <= @{paramIndex}");
                break;

            case "startswith":
                EnsureString(fieldInfo);
                sb.Append($"{fieldInfo.PropertyName}.StartsWith(@{paramIndex})");
                break;

            case "contains":
                EnsureString(fieldInfo);
                sb.Append($"{fieldInfo.PropertyName}.Contains(@{paramIndex})");
                break;

            case "endswith":
                EnsureString(fieldInfo);
                sb.Append($"{fieldInfo.PropertyName}.EndsWith(@{paramIndex})");
                break;

            default:
                throw new ArgumentException($"Unsupported operator '{op}'.");
        }
    }

    private static void EnsureString(FilterFieldInfo info)
    {
        var nn = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;
        if (nn != typeof(string))
            throw new ArgumentException($"Operator is only valid for strings: '{info.PropertyName}'.");
    }

    private static object? ConvertValue(object? raw, Type targetType)
    {
        if (raw is JsonElement je)
        {
            raw = je.ValueKind switch
            {
                JsonValueKind.Null => null,
                JsonValueKind.String => je.GetString(),
                JsonValueKind.Number => je.TryGetInt64(out var l) ? l : je.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                _ => je.ToString()
            };
        }

        if (raw is null)
            return null;

        var nn = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (nn == typeof(Guid))
        {
            if (raw is Guid g)
                return g;

            if (raw is string s && Guid.TryParse(s, out var parsed))
                return parsed;

            throw new ArgumentException($"Invalid GUID value: '{raw}'");
        }

        if (nn.IsEnum)
        {
            if (raw is string s)
                return Enum.Parse(nn, s, ignoreCase: true);

            return Enum.ToObject(nn, Convert.ToInt32(raw, CultureInfo.InvariantCulture));
        }

        if (nn.IsInstanceOfType(raw))
            return raw;

        return Convert.ChangeType(raw, nn, CultureInfo.InvariantCulture);
    }

    private static Array ConvertList(object? raw, Type elementType)
    {
        if (raw is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Null)
                return Array.CreateInstance(elementType, 0);

            if (je.ValueKind != JsonValueKind.Array)
                throw new ArgumentException("For 'in'/'notin', value must be a JSON array.");

            var items = je.EnumerateArray().ToList();
            var arr = Array.CreateInstance(elementType, items.Count);

            for (int i = 0; i < items.Count; i++)
            {
                var converted = ConvertValue(items[i], elementType);
                if (converted is null)
                    throw new ArgumentException("Null elements are not supported in 'in'/'notin' arrays.");

                arr.SetValue(converted, i);
            }

            return arr;
        }

        // If someone passes a .NET list already
        if (raw is System.Collections.IEnumerable enumerable && raw is not string)
        {
            var list = new List<object?>();
            foreach (var item in enumerable)
                list.Add(item);

            var arr = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                var converted = ConvertValue(list[i], elementType) ?? 
                    throw new ArgumentException("Null elements are not supported in 'in'/'notin' arrays.");

                arr.SetValue(converted, i);
            }

            return arr;
        }

        throw new ArgumentException("For 'in'/'notin', value must be a JSON array.");
    }
}
