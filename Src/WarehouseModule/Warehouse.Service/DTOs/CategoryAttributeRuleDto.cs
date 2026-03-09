namespace NGErp.Warehouse.Service.DTOs;

// Details endpoint → Full nested DTO 
public record CategoryAttributeRuleDto(
    Guid Id,
    bool IsItemAttribute,
    bool IsRequired,
    bool IsStockDimension,
    CategoryDto Category,
    AttributeDto Attribute
);

public record CategoryAttributeRuleSlimDto(
    Guid Id,
    bool IsItemAttribute,
    bool IsRequired,
    bool IsStockDimension,
    CategorySlimDto Category,
    AttributeSlimDto Attribute
);

// List endpoints → Flat DTO
public record CategoryAttributeRuleListDto(
    Guid Id,
    bool IsItemAttribute,
    bool IsRequired,
    bool IsStockDimension,
    string CategoryTitle,
    string AttributeTitle
);

public class CreateCategoryAttributeRuleDto
{
    public required bool IsItemAttribute { get; set; }
    public required bool IsStockDimension { get; set; }
    public required bool IsRequired { get; set; }
    public required Guid AttributeId { get; set; }
    public int? SortOrder { get; set; }
}

public class PatchCategoryAttributeRuleDto
{
    public bool? IsItemAttribute { get; set; }
    public bool? IsStockDimension { get; set; }
    public bool? IsRequired { get; set; }
    public int? SortOrder { get; set; }
}
