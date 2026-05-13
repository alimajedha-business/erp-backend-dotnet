namespace NGErp.Warehouse.Service.DTOs;

public record ItemUnitOfMeasurementDto(
    UnitOfMeasurementSlimDto UnitOfMeasurement,
    int UnitOrder
);

public class CreateItemUnitOfMeasurementDto
{
    public required Guid UnitOfMeasurementId { get; set; }
    public required int UnitOrder { get; set; }
}

public class PatchItemUnitOfMeasurementDto
{
    public Guid? UnitOfMeasurementId { get; set; }
    public int? UnitOrder { get; set; }
}
