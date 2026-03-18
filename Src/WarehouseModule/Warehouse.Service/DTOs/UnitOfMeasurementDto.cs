namespace NGErp.Warehouse.Service.DTOs;

public record UnitOfMeasurementDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol,
    MeasurementDimensionDto MeasurementDimension
);

public record UnitOfMeasurementListDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol
);

public record UnitOfMeasurementTitleDto(string Title);

public class CreateUnitOfMeasurementDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required string Symbol { get; set; }
    public required Guid MeasurementDimensionId { get; set; }
}

public class PatchUnitOfMeasurementDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public string? Symbol { get; set; }
    public Guid? MeasurementDimensionId { get; set; }
}
