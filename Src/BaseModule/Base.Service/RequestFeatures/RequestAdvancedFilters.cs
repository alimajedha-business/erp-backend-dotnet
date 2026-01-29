namespace NGErp.Base.Service.RequestFeatures;

public record RequestAdvancedFilters
{
    public string? Predicate { get; set; }
    public object[]? Args { get; set; }

    public void Deconstruct(out string? predicate, out object[]? args)
        => (predicate, args) = (Predicate, Args);
}
