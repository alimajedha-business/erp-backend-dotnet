namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineDto(
    Guid Id,
    int Sequence,
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
    IReadOnlyList<ReceiptLineMeasurementValueDto> ReceiptLineMeasurementValues,
    IReadOnlyList<ReceiptLineAttributeValueDto> ReceiptLineAttributeValues,
    IReadOnlyList<ReceiptFieldValueDto> ReceiptFieldValues
);

public class CreateReceiptLineDto
{
    public int Sequence { get; set; }
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

    public List<CreateReceiptLineMeasurementValueDto> ReceiptLineMeasurementValues { get; set; } = [];
    public List<CreateReceiptLineAttributeValueDto> ReceiptLineAttributeValues { get; set; } = [];
    public List<CreateReceiptFieldValueDto> ReceiptFieldValues { get; set; } = [];
}
