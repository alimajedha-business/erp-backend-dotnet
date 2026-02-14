namespace NGErp.Base.Service.DTOs;

public record FilterNodeDto
{
    // Discriminator: "group" | "condition"
    public string Type { get; init; } = default!;

    // Group
    public string? Op { get; init; }
    public List<FilterNodeDto>? Children { get; init; }

    // Condition
    public string? Field { get; init; }
    public string? Operator { get; init; }
    public object? Value { get; init; }
}