namespace NGErp.Warehouse.Service.DTOs;

public record AttributeEnumValueDto(
    Guid Id,
    string Code,
    string Label,
    AttributeDto Attribute
);

public record AttributeEnumValueListDto(
    Guid Id,
    string Code,
    string Label,
    string AttributeTitle
);

public class CreateAttributeEnumValueDto
{
    public required string Code { get; set; } = default!;
    public required string Label { get; set; } = default!;
}

public class PatchAttributeEnumValueDto
{
    public string? Code { get; set; }
    public string? Label { get; set; }
}