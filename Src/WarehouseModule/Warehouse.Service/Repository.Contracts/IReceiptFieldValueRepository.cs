using System.Linq.Expressions;

using AutoMapper;

using NGErp.Base.Domain.Entities;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IReceiptFieldValueRepository
{
    Task<ListQueryResult<ReceiptFieldValueReferenceDto>> FilterByQ<TEntity>(
        RequestParameters requestParameters,
        IConfigurationProvider mapperConfig,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken ct = default
    ) where TEntity : BaseEntity;
}
