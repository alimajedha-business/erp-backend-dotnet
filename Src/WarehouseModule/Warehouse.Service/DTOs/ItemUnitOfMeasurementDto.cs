namespace NGErp.Warehouse.Service.DTOs;

public record ItemUnitOfMeasurementDto(
    ItemDto Item,
    UnitOfMeasurementDto UnitOfMeasurement,
    int UnitOrder
);

public class CreateItemUnitOfMeasurementDto
{
    public required Guid ItemId { get; set; }
    public required Guid UnitOfMeasurementId { get; set; }
    public required int UnitOrder { get; set; }
}

public class PatchItemUnitOfMeasurementDto
{
    public Guid? ItemId { get; set; }
    public Guid? UnitOfMeasurementId { get; set; }
    public int? UnitOrder { get; set; }
}
