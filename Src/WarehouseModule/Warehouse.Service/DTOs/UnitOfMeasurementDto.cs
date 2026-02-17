namespace NGErp.Warehouse.Service.DTOs;

public record UnitOfMeasurementDto(
    Guid Id,
    string Dimension,
    string Title,
    string Symbol,
    bool IsDiscrete
);

public record UnitOfMeasurementTitleDto(string Title);

public class CreateUnitOfMeasurementDto
{
    public required string Dimension { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public required string Symbol { get; set; }
    public bool IsDiscrete { get; set; }
}

public class PatchUnitOfMeasurementDto
{
    public string? Dimension { get; set; }
    public string? Title { get; set; }
    public string? Symbol { get; set; }
    public bool? IsDiscrete { get; set; }
}
