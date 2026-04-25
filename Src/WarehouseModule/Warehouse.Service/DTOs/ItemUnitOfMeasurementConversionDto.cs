namespace NGErp.Warehouse.Service.DTOs;

public record ItemUnitOfMeasurementConversionDto(
    Guid UnitOfMeasurementId,
    string ConversionEquation
);

public class CreateItemUnitOfMeasurementConversionDto
{
    public Guid ItemId { get; set; }
    public Guid UnitOfMeasurementId { get; set; }
    public required string ConversionEquation { get; set; }
}
