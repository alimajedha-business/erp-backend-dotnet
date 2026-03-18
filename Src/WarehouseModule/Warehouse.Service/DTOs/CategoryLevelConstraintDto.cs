namespace NGErp.Warehouse.Service.DTOs;

public record CategoryLevelConstraintDto(
    Guid Id,
    int LevelNo,
    int CodeLength
);

public class CreateCategoryLevelConstraintDto
{
    public required int LevelNo { get; set; }
    public required int CodeLength { get; set; }
}

public class PatchCategoryLevelConstraintDto
{
    public int? CodeLength { get; set; }
}
