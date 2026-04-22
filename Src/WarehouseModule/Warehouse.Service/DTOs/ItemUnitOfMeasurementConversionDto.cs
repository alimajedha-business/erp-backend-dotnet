namespace NGErp.Warehouse.Service.DTOs;

public class CreateItemUnitOfMeasurementConversionDto
{
    public Guid ItemId { get; set; }
    public Guid UnitOfMeasurementId { get; set; }
    public required string ConversionEquation { get; set; }
}
