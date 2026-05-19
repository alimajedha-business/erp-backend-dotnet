using System.Linq.Expressions;

using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class RemittanceRepository(MainDbContext context) :
    RepositoryWithCompany<Remittance>(context),
    IRemittanceRepository
{
    public override async Task<Remittance?> SingleOrDefaultAsync(
        Expression<Func<Remittance, bool>> predicate,
        bool trackChanges,
        CancellationToken ct = default
    )
    {
        var query = trackChanges
            ? _dbSet.AsSplitQuery()
            : _dbSet.AsNoTracking().AsSplitQuery();

        return await WithIncludes(query).SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<Remittance> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithListIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    public void RemoveRemittanceFieldValues(IEnumerable<RemittanceFieldValue> fieldValues)
    {
        _context.Set<RemittanceFieldValue>().RemoveRange(fieldValues);
    }

    public void RemoveRemittanceLineAttributeValues(
        IEnumerable<RemittanceLineAttributeValue> attributeValues
    )
    {
        _context.Set<RemittanceLineAttributeValue>().RemoveRange(attributeValues);
    }

    public void RemoveRemittanceLineMeasurementValues(
        IEnumerable<RemittanceLineMeasurementValue> measurementValues
    )
    {
        _context.Set<RemittanceLineMeasurementValue>().RemoveRange(measurementValues);
    }

    public void RemoveRemittanceLines(IEnumerable<RemittanceLine> remittanceLines)
    {
        _context.Set<RemittanceLine>().RemoveRange(remittanceLines);
    }

    public async Task DeleteRemittanceGraphAsync(Guid companyId, Guid id, CancellationToken ct)
    {
        var remittance = await SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        );

        if (remittance is null)
            return;

        RemoveRemittanceLineMeasurementValues(
            remittance.RemittanceLines.SelectMany(e => e.RemittanceLineMeasurementValues)
        );
        RemoveRemittanceLineAttributeValues(
            remittance.RemittanceLines.SelectMany(e => e.RemittanceLineAttributeValues)
        );
        RemoveRemittanceFieldValues(remittance.RemittanceFieldValues);
        RemoveRemittanceLines(remittance.RemittanceLines);
        _dbSet.Remove(remittance);

        await SaveChangesAsync(ct);
    }

    public async Task<int> GetNextNumberAsync(Guid companyId, CancellationToken ct)
    {
        var maxNumber = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.Number, ct);

        return (maxNumber ?? 0) + 1;
    }

    private static IQueryable<Remittance> WithIncludes(IQueryable<Remittance> query)
    {
        return query
            .Include(e => e.RemittanceType)
            .Include(e => e.RemittanceFieldValues)
            .Include(e => e.RemittanceLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.PreferredMassUnit)
            .Include(e => e.RemittanceLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.PreferredVolumeUnit)
            .Include(e => e.RemittanceLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.RemittanceFieldValues)
            .Include(e => e.RemittanceLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.RemittanceLineMeasurementValues)
            .Include(e => e.RemittanceLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.RemittanceLineAttributeValues);
    }

    private static IQueryable<Remittance> WithListIncludes(IQueryable<Remittance> query)
    {
        return query
            .Include(e => e.RemittanceType)
            .Include(e => e.RemittanceLines);
    }
}
