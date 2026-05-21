namespace NGErp.Base.Domain.EntityTypeRegistration;

public class MenuDefinition
{
    public required string NameFa { get; init; }
    public required string NameEn { get; init; }
    public short Order { get; init; }
    public string? Link { get; init; }
    public string? ShortKey { get; init; }
    public bool NewTab { get; init; }
    public string? StandardPage { get; init; }
    public string? Meta { get; init; }
    public string? EntityTypeKey { get; init; }
    public string? EntityTypeCommandKey { get; init; }
    public List<MenuDefinition> Children { get; init; } = new();
}
