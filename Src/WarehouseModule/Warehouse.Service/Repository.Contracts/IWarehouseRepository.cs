using System.Linq.Expressions;

using AutoMapper;

using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseRepository :
    IRepositoryWithCompany<Domain.Entities.Warehouse>
{
    Task<WarehouseDto?> SingleOrDefaultAsync(
        Expression<Func<Domain.Entities.Warehouse, bool>> predicate,
        IConfigurationProvider mapperConfig,
        bool trackChanges = true,
        CancellationToken ct = default
    );

    Task<int> GetNextCodeAsync(Guid companyId, CancellationToken ct);
}
