namespace NGErp.Warehouse.Service.DTOs;

public record AttributeEnumValueDto(
    Guid Id,
    string Code,
    string Label,
    string AttributeTitle
);

public class CommandAttributeEnumValueDto
{
    public required string Code { get; set; } = default!;
    public required string Label { get; set; } = default!;
}

public class CreateAttributeEnumValueDto : CommandAttributeEnumValueDto { }

public class UpdateAttributeEnumValueDto : CommandAttributeEnumValueDto { }