using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryLevelConstraintService : IBaseServiceWithCompany<
    CategoryLevelConstraint,
    CategoryLevelConstraintDto,
    CategoryLevelConstraintParameters,
    ICategoryLevelConstraintRepository,
    WarehouseResource
>
{
    Task<CategoryLevelConstraintDto> GetByLevelNoAsync(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    );

    Task<int> GetNextLevelAsync(
        Guid companyId,
        CancellationToken ct
    );
}
