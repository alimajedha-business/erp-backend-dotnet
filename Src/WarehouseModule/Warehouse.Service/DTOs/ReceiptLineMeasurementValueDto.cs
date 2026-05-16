namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineMeasurementValueDto(
    Guid Id,
    Guid ItemUnitOfMeasurementId,
    decimal Quantity
);

public class CreateReceiptLineMeasurementValueDto
{
    public Guid ItemUnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }
}
