namespace NGErp.Warehouse.Service.DTOs;

public record CategoryDto(string Code, string Title, int LevelNo, bool IsLastLevel);
public record CreateCategoryDto();
public record UpdateCategoryDto();