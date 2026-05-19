namespace NGErp.Warehouse.Service.DTOs;

public record RemittanceLineMeasurementValueDto(
    Guid Id,
    Guid ItemUnitOfMeasurementId,
    decimal Quantity
);

public class CreateRemittanceLineMeasurementValueDto
{
    public Guid ItemUnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }
}
