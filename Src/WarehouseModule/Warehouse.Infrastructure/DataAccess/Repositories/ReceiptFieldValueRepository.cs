using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Domain.Entities;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ReceiptFieldValueRepository(MainDbContext context) :
    IReceiptFieldValueRepository
{
    protected readonly MainDbContext _context = context;

    public async Task<ListQueryResult<ReceiptFieldValueReferenceDto>> FilterByQ<TEntity>(
        RequestParameters requestParameters,
        IConfigurationProvider mapperConfig,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken ct = default
    ) where TEntity : BaseEntity
    {
        var query = _context.Set<TEntity>().AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ProjectTo<ReceiptFieldValueReferenceDto>(mapperConfig)
            .ToListAsync(ct);

        return new ListQueryResult<ReceiptFieldValueReferenceDto>(items, totalCount);
    }
}
