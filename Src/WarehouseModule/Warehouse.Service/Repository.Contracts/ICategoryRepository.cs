using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryRepository : IRepositoryWithCompany<Category> 
{
    Task<ListQueryResult<Category>> GetDirectChildrenAsync(
        Guid companyId,
        Guid id,
        CategoryParameters categoryParameters,
        CancellationToken ct
    );
}