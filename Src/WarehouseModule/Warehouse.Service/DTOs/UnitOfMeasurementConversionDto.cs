namespace NGErp.Warehouse.Service.DTOs;

public record UnitOfMeasurementConversionDto(
    Guid Id,
    decimal Factor,
    Guid FromUnitOfMeasurementId,
    Guid ToUnitOfMeasurementId
);

public class CreateUnitOfMeasurementConversionDto
{
    public required decimal Factor { get; set; }
    public required Guid FromUnitOfMeasurementId { get; set; }
    public required Guid ToUnitOfMeasurementId { get; set; }
}

public class PatchUnitOfMeasurementConversionDto
{
    public decimal? Factor { get; set; }
    public Guid? FromUnitOfMeasurementId { get; set; }
    public Guid? ToUnitOfMeasurementId { get; set; }
}
