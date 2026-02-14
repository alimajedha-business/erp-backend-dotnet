namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(
    Guid Id,
    string Code,
    string Title,
    int LevelNo,
    bool IsLastLevel
);

public class CreateCategoryDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public required int LevelNo { get; set; }
    public bool IsLastLevel { get; set; }
    public string CategoryPath { get; set; } = default!;
    public Guid ParentCategoryId { get; set; }
}

public class PatchCategoryDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsLastLevel { get; set; }
    public string? CategoryPath { get; set; }
}