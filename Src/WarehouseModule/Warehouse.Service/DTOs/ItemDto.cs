namespace NGErp.Warehouse.Service.DTOs;

public record ItemDto(
    Guid Id,
    string Code,
    string Title,
    string Sku,
    bool IsActive
);

public class CreateItemDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public required string Sku { get; set; } = default!;
    public bool IsActive { get; set; }
    public required Guid CategoryId { get; set; }
}

public class PatchItemDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsActive { get; set; }
}
