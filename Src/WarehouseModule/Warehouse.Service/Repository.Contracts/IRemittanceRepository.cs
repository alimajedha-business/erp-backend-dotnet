using System.Linq.Expressions;

using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IRemittanceRepository : IRepositoryWithCompany<Remittance>
{
    new Task<Remittance?> SingleOrDefaultAsync(
        Expression<Func<Remittance, bool>> predicate,
        bool trackChanges,
        CancellationToken ct = default
    );

    new IQueryable<Remittance> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    );

    void RemoveRemittanceFieldValues(IEnumerable<RemittanceFieldValue> fieldValues);

    void RemoveRemittanceLineAttributeValues(
        IEnumerable<RemittanceLineAttributeValue> attributeValues
    );

    void RemoveRemittanceLineMeasurementValues(
        IEnumerable<RemittanceLineMeasurementValue> measurementValues
    );

    void RemoveRemittanceLines(IEnumerable<RemittanceLine> remittanceLines);

    Task DeleteRemittanceGraphAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextNumberAsync(
        Guid companyId,
        CancellationToken ct
    );
}
