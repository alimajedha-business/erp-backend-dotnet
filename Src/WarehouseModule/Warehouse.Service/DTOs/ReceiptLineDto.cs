namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineDto(
    Guid Id,
    int RowNumber,
    Guid ItemId,
    Guid UnitOfMeasurementId,
    decimal Quantity,
    decimal? UnitPrice,
    decimal? TotalPrice,
    IReadOnlyList<ReceiptLineAttributeValueDto> ReceiptLineAttributeValues,
    IReadOnlyList<ReceiptFieldValueDto> ReceiptFieldValues
);

public class CreateReceiptLineDto
{
    public int RowNumber { get; set; }
    public Guid ItemId { get; set; }
    public Guid UnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }

    public List<CreateReceiptLineAttributeValueDto> ReceiptLineAttributeValues { get; set; } = [];
    public List<CreateReceiptFieldValueDto> ReceiptFieldValues { get; set; } = [];
}
