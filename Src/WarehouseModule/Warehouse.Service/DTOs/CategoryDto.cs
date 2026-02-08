namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(
    Guid Id,
    string Code,
    string Title,
    int LevelNo,
    bool IsLastLevel
);

public class CommandCategoryDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public bool IsLastLevel { get; set; }
    public Guid ParentCategoryId { get; set; }
}

public class CreateCategoryDto : CommandCategoryDto
{
    public required int LevelNo { get; set; }
    public string CategoryPath { get; set; } = default!;
}

public class UpdateCategoryDto : CommandCategoryDto { }