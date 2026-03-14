namespace NGErp.Warehouse.Service.DTOs;

public record MeasurementDimensionDto(
    Guid Id,
    int Code,
    string Title,
    bool IsDiscrete
);

public class CreateMeasurementDimensionDto
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required bool IsDiscrete { get; set; } = false;
}

public class PatchMeasurementDimensionDto
{
    public int? Code { get; set; }
    public string? Title { get; set; }
    public bool? IsDiscrete { get; set; }
}