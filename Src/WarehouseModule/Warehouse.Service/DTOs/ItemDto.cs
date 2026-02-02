namespace NGErp.Warehouse.Service.DTOs;

public record ItemDto(
    Guid Id,
    string Code,
    string Title,
    string Sku,
    bool IsActive
);

public class CommandItemDto
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
}

public class CreateItemDto : CommandItemDto
{
    public string Sku { get; set; } = default!;
}

public class UpdateItemDto : CommandItemDto { }
