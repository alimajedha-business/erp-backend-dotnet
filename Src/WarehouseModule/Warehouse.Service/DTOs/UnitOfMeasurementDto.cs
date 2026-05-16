namespace NGErp.Warehouse.Service.DTOs;

public record UnitOfMeasurementDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol
);

public record UnitOfMeasurementSlimDto(
    Guid Id,
    int Code,
    string Title
);

public class CreateUnitOfMeasurementDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required string Symbol { get; set; }
}

public class PatchUnitOfMeasurementDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public string? Symbol { get; set; }
}
