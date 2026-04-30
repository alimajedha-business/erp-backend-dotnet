using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryRepository : IRepositoryWithCompany<Category>
{
    Task<CategoryCodeDto?> GetCategoryCodeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}