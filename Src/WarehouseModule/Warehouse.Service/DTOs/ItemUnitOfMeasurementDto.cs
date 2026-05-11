namespace NGErp.Warehouse.Service.DTOs;

public record ItemUnitOfMeasurementDto(
    UnitOfMeasurementSlimDto UnitOfMeasurement,
    int UnitOrder,
    decimal? Weigh,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? CubeVolume,
    UnitSlimDto? PreferredMassUnit,
    UnitSlimDto? PreferredLengthUnit,
    UnitSlimDto? PreferredVolumeUnit
);

public class CreateItemUnitOfMeasurementDto
{
    public required Guid UnitOfMeasurementId { get; set; }
    public required int UnitOrder { get; set; }
    public decimal? Weigh { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? CubeVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
}

public class PatchItemUnitOfMeasurementDto
{
    public Guid? UnitOfMeasurementId { get; set; }
    public int? UnitOrder { get; set; }
    public decimal? Weigh { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? CubeVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
}
