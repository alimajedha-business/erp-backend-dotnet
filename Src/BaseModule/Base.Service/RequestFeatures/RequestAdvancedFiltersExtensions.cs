using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

using NGErp.Base.Service.Services;

namespace NGErp.Base.Service.RequestFeatures;

public static class RequestAdvancedFiltersExtensions
{
    public static RequestAdvancedFilters MergeQueryFilters<TEntity>(
        this RequestAdvancedFilters? existing,
        IReadOnlyDictionary<string, string> queryFilters)
    {
        var (newPredicate, newArgs) = BuildPredicateArgs<TEntity>(queryFilters);

        // nothing new
        if (string.IsNullOrWhiteSpace(newPredicate))
            return existing ?? new RequestAdvancedFilters();

        // no existing filters -> create new
        if (existing == null || string.IsNullOrWhiteSpace(existing.Predicate))
        {
            return new RequestAdvancedFilters
            {
                Predicate = newPredicate,
                Args = newArgs
            };
        }

        // append to existing -> shift @ indexes
        var offset = existing.Args?.Length ?? 0;
        var shiftedNewPredicate = ShiftIndexes(newPredicate, offset);

        return new RequestAdvancedFilters
        {
            Predicate = $"({existing.Predicate}) and ({shiftedNewPredicate})",
            Args = [.. (existing.Args ?? []), .. newArgs]
        };
    }

    private static (string predicate, object[] args) BuildPredicateArgs<TEntity>(
        IReadOnlyDictionary<string, string> queryFilters)
    {
        if (queryFilters == null || queryFilters.Count == 0)
            return (string.Empty, Array.Empty<object>());

        var parts = new List<string>();
        var args = new List<object>();

        foreach (var (key, raw) in queryFilters)
        {
            if (string.IsNullOrWhiteSpace(key) || raw is null)
                continue;

            // Case-insensitive property lookup
            var prop = typeof(TEntity).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (prop == null || !prop.CanRead)
                continue;

            var typed = ConvertString(raw, prop.PropertyType);
            args.Add(typed!);

            // Dynamic LINQ equality
            parts.Add($"{prop.Name} == @{args.Count - 1}");
        }

        return (string.Join(" and ", parts), args.ToArray());
    }

    private static object? ConvertString(string value, Type targetType)
    {
        var underlying = Nullable.GetUnderlyingType(targetType);
        var nonNull = underlying ?? targetType;

        if (nonNull == typeof(string))
            return value;
        if (nonNull == typeof(Guid))
            return Guid.Parse(value);
        if (nonNull.IsEnum)
            return Enum.Parse(nonNull, value, ignoreCase: true);

        return Convert.ChangeType(value, nonNull, CultureInfo.InvariantCulture);
    }

    private static string ShiftIndexes(string predicate, int offset)
    {
        return RegexHelpers.DynamicLinqArgRegex().Replace(predicate, m =>
        {
            var oldIndex = int.Parse(m.Groups[1].Value);
            return "@" + (oldIndex + offset);
        });
    }
}
