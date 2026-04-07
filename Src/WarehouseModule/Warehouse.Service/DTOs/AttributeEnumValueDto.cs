namespace NGErp.Warehouse.Service.DTOs;

public record AttributeEnumValueDto(
    Guid Id,
    int Code,
    string Label,
    AttributeDto Attribute
);

public record AttributeEnumValueListDto(
    Guid Id,
    int Code,
    string Label,
    string AttributeTitle
);

public class CreateAttributeEnumValueDto
{
    public required int Code { get; set; }
    public required string Label { get; set; }
    public Guid AttributeId { get; set; }
}

public class PatchAttributeEnumValueDto
{
    public int? Code { get; set; }
    public string? Label { get; set; }
}