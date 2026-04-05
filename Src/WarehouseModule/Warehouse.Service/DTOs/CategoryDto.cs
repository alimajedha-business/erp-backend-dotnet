namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(
    Guid Id,
    string Code,
    string Title,
    int LevelNo,
    bool HasNextLevel
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
    public bool HasNextLevel { get; set; } = false;
    public Guid? ParentCategoryId { get; set; }
}

public class PatchCategoryDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? HasNextLevel { get; set; }
}