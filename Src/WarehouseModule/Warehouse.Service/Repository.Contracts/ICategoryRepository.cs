using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryRepository : IRepositoryWithCompany<Category> 
{
    Task<ListQueryResult<Category>> GetAllAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
}