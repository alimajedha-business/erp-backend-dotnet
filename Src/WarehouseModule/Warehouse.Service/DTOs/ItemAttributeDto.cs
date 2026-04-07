namespace NGErp.Warehouse.Service.DTOs;

public record ItemAttributeDto(
    ItemDto Item,
    AttributeDto Attribute
);

public class CreateItemAttributeDto
{
    public required Guid ItemId { get; set; }
    public required Guid AttributeId { get; set; }
}

public class PatchItemAttributeDto
{
    public Guid? ItemId { get; set; }
    public Guid? AttributeId { get; set; }
}
