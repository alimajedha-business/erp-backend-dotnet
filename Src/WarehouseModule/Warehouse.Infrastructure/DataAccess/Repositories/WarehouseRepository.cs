using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseRepository(MainDbContext context) :
    RepositoryWithCompany<Domain.Entities.Warehouse>(context),
    IWarehouseRepository
{
    public async Task<WarehouseDto?> SingleOrDefaultAsync(
        Expression<Func<Domain.Entities.Warehouse, bool>> predicate,
        IConfigurationProvider mapperConfig,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .AsSplitQuery();

        return await query
            .Where(predicate)
            .Include(i => i.WarehouseType)
            .ProjectTo<WarehouseDto>(mapperConfig)
            .SingleOrDefaultAsync(ct);
    }

    public async Task<int> GetNextCodeAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}