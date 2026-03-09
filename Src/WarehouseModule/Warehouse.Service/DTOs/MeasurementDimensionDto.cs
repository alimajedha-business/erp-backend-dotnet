namespace NGErp.Warehouse.Service.DTOs;

public record MeasurementDimensionDto(
    Guid Id,
    string Code,
    string Title,
    bool IsDiscrete
);

public class CreateMeasurementDimensionDto
{
    public required string Code { get; set; } = default!;
    public required string Title { get; set; } = default!;
    public bool IsDiscrete { get; set; }
}

public class PatchMeasurementDimensionDto
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsDiscrete { get; set; }
}