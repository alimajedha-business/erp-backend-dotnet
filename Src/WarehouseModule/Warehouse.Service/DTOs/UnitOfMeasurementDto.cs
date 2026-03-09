namespace NGErp.Warehouse.Service.DTOs;

public record UnitOfMeasurementDto(
    Guid Id,
    string Code,
    string Title,
    string Symbol,
    MeasurementDimensionDto MeasurementDimension
);

public record UnitOfMeasurementListDto(
    Guid Id,
    string Code,
    string Title,
    string Symbol
);

public record UnitOfMeasurementTitleDto(string Title);

public class CreateUnitOfMeasurementDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public required string Symbol { get; set; }
    public bool IsDiscrete { get; set; }
    public required Guid MeasurementDimensionId { get; set; }
}

public class PatchUnitOfMeasurementDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Symbol { get; set; }
    public bool? IsDiscrete { get; set; }
    public Guid? MeasurementDimensionId { get; set; }
}
