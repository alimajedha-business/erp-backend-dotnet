namespace NGErp.Warehouse.Service.DTOs;

// Details endpoint → Full nested DTO 
public record UnitOfMeasurementConversionDto(
    Guid Id,
    decimal Factor,
    UnitOfMeasurementDto FromUnitOfMeasurement,
    UnitOfMeasurementDto ToUnitOfMeasurement
);

public record UnitOfMeasurementConversionSlimDto(
    Guid Id,
    decimal Factor,
    UnitOfMeasurementTitleDto FromUnitOfMeasurement,
    UnitOfMeasurementTitleDto ToUnitOfMeasurement
);

// List endpoints → Flat DTO
public record UnitOfMeasurementConversionListDto(
    Guid Id,
    decimal Factor,
    string FromUnitOfMeasurementTitle,
    string ToUnitOfMeasurementTitle
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
