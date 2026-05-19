using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts.Models;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IReceiptRepository : IRepositoryWithCompany<Receipt>
{
    void RemoveReceiptFieldValues(IEnumerable<ReceiptFieldValue> fieldValues);

    void RemoveReceiptLineAttributeValues(
        IEnumerable<ReceiptLineAttributeValue> attributeValues
    );

    void RemoveReceiptLineMeasurementValues(
        IEnumerable<ReceiptLineMeasurementValue> measurementValues);

    void RemoveReceiptLines(IEnumerable<ReceiptLine> receiptLines);

    Task DeleteReceiptGraphAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextNumberAsync(Guid companyId, CancellationToken ct);

    Task<IReadOnlyList<WarehouseLocationOccupancy>> GetLocationOccupanciesAsync(
        Guid companyId,
        IReadOnlyCollection<Guid> warehouseLocationIds,
        Guid? excludedReceiptId,
        CancellationToken ct
    );
}
