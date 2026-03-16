using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryLevelConstraintRepository : 
    IRepositoryWithCompany<CategoryLevelConstraint>
{
    Task<CategoryLevelConstraint?> GetByLevelNo(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    );
}