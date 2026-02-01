namespace NGErp.Base.Domain.EntitySchemas;

public sealed record FilterFieldInfo(
    string PropertyName,
    Type PropertyType,
    HashSet<string> AllowedOps
);

public sealed class FilterSchema
{
    public Dictionary<string, FilterFieldInfo> Fields { get; } =
        new(StringComparer.OrdinalIgnoreCase);
}
