using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitOfMeasurementConversionRepository(MainDbContext context) :
    RepositoryWithCompany<UnitOfMeasurementConversion>(context),
    IUnitOfMeasurementConversionRepository
{
    public override Task<UnitOfMeasurementConversion?> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges
    )
    {
        var query = trackChanges ? _context.Set<UnitOfMeasurementConversion>()
                                 : _context.Set<UnitOfMeasurementConversion>().AsNoTracking();

        return query
            .Where(e => e.CompanyId == companyId && e.Id == id)
            .Include(e => e.FromUnitOfMeasurement)
            .Include(e => e.ToUnitOfMeasurement)
            .FirstOrDefaultAsync(ct);
    }

    public override async Task<ListQueryResult<UnitOfMeasurementConversion>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<UnitOfMeasurementConversion> query = _context
            .Set<UnitOfMeasurementConversion>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Include(e => e.FromUnitOfMeasurement)
            .Include(e => e.ToUnitOfMeasurement)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<UnitOfMeasurementConversion>(items, totalCount);
    }
}
