namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(Guid Id, string Code, string Title, int LevelNo, bool IsLastLevel);

public class CommandCategoryDto
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int LevelNo { get; set; }
    public bool IsLastLevel { get; set; }
    public string CategoryPath { get; set; } = default!;
    public Guid ParentCategoryId { get; set; }
}


public class CreateCategoryDto : CommandCategoryDto { }

public class UpdateCategoryDto : CommandCategoryDto { }