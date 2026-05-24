namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceLineDto(
    Guid Id,
    int RowNumber,
    Guid ItemId,
    Guid WarehouseLocationId,
    decimal? Weight,
    decimal? Volume,
    Guid? PreferredMassUnitId,
    Guid? PreferredVolumeUnitId,
    decimal? UnitPrice,
    decimal? TotalPrice,
    string? BatchNumber,
    string? SerialNumber,
    DateOnly? ExpiryDate,
    string? Description,
    IReadOnlyList<RemittanceLineMeasurementValueDto> RemittanceLineMeasurementValues,
    IReadOnlyList<RemittanceLineAttributeValueDto> RemittanceLineAttributeValues,
    IReadOnlyList<RemittanceFieldValueDto> RemittanceFieldValues
);

public class CreateRemittanceLineDto
{
    public int RowNumber { get; set; }
    public Guid ItemId { get; set; }
    public Guid WarehouseLocationId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Volume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? BatchNumber { get; set; }
    public string? SerialNumber { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public string? Description { get; set; }

    public List<CreateRemittanceLineMeasurementValueDto> RemittanceLineMeasurementValues { get; set; } = [];
    public List<CreateRemittanceLineAttributeValueDto> RemittanceLineAttributeValues { get; set; } = [];
    public List<CreateRemittanceFieldValueDto> RemittanceFieldValues { get; set; } = [];
}
