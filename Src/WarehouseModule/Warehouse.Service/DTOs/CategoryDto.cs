namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(
    Guid Id,
    string Code,
    string Title,
    int LevelNo,
    bool IsLastLevel
);

public record CategorySlimDto(
    Guid Id,
    string Code,
    string Title
);

public class CreateCategoryDto
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required int LevelNo { get; set; }
    public required bool IsLastLevel { get; set; } = false;
    public required string CategoryPath { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

public class PatchCategoryDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsLastLevel { get; set; }
    public string? CategoryPath { get; set; }
}