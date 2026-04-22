namespace NGErp.Warehouse.Service.DTOs;

public record ItemTypeDto(
    Guid Id,
    int Code,
    string Title
);

public record ItemTypeSlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateItemTypeDto
{
    public int Code { get; set; }
    public required string Title { get; set; }
}

public class PatchItemTypeDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
}
