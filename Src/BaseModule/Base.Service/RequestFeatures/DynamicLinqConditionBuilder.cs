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

        var normalizedOp = op.ToLowerInvariant();

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

        var paramIndex = prms.Count;
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
}
