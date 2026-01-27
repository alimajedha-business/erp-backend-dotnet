namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(Guid Id, string Code, string Title, int LevelNo, bool IsLastLevel);
public class CreateCategoryDto
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int LevelNo { get; set; }
    public bool IsLastLevel { get; set; }
    public string CategoryPath { get; set; } = default!;
    public Guid ParentCategoryId { get; set; }
    public Guid CompanyId { get; set; }
}
public record UpdateCategoryDto();