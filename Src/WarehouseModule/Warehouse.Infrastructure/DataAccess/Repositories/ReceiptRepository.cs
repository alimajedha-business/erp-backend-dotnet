using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptRepository(MainDbContext context) :
    RepositoryWithCompany<Receipt>(context),
    IReceiptRepository
{
    public override async Task<Receipt?> SingleOrDefaultAsync(
        Expression<Func<Receipt, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = trackChanges
            ? _dbSet.AsSplitQuery()
            : _dbSet.AsNoTracking().AsSplitQuery();

        return await WithIncludes(query)
            .SingleOrDefaultAsync(predicate, ct);
    }

    public override IQueryable<Receipt> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return WithListIncludes(_dbSet.AsNoTracking())
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    public void RemoveReceiptFieldValues(IEnumerable<ReceiptFieldValue> fieldValues)
    {
        _context.Set<ReceiptFieldValue>().RemoveRange(fieldValues);
    }

    public void RemoveReceiptLineAttributeValues(
        IEnumerable<ReceiptLineAttributeValue> attributeValues
    )
    {
        _context.Set<ReceiptLineAttributeValue>().RemoveRange(attributeValues);
    }

    public void RemoveReceiptLineMeasurementValues(
        IEnumerable<ReceiptLineMeasurementValue> measurementValues
    )
    {
        _context.Set<ReceiptLineMeasurementValue>().RemoveRange(measurementValues);
    }

    public void RemoveReceiptLines(IEnumerable<ReceiptLine> receiptLines)
    {
        _context.Set<ReceiptLine>().RemoveRange(receiptLines);
    }

    public async Task DeleteReceiptGraphAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var receipt = await SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        );

        if (receipt is null)
            return;

        RemoveReceiptLineAttributeValues(
            receipt.ReceiptLines.SelectMany(e => e.ReceiptLineAttributeValues)
        );
        RemoveReceiptLineMeasurementValues(
            receipt.ReceiptLines.SelectMany(e => e.ReceiptLineMeasurementValues)
        );
        RemoveReceiptFieldValues(receipt.ReceiptFieldValues);
        RemoveReceiptLines(receipt.ReceiptLines);
        _dbSet.Remove(receipt);

        await SaveChangesAsync(ct);
    }

    public async Task<int> GetNextNumberAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.Number, ct);

        return (maxCode ?? 0) + 1;
    }

    private static IQueryable<Receipt> WithIncludes(IQueryable<Receipt> query)
    {
        return query
            .Include(e => e.ReceiptType)
            .Include(e => e.ReceiptFieldValues)
            .Include(e => e.ReceiptLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.ReceiptFieldValues)
            .Include(e => e.ReceiptLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.ReceiptLineAttributeValues)
            .Include(e => e.ReceiptLines.OrderBy(l => l.RowNumber))
                .ThenInclude(e => e.ReceiptLineMeasurementValues);
    }

    private static IQueryable<Receipt> WithListIncludes(IQueryable<Receipt> query)
    {
        return query
            .Include(e => e.ReceiptType)
            .Include(e => e.ReceiptLines);
    }
}
