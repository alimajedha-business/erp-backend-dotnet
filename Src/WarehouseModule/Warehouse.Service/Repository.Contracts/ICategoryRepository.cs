using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category> 
{
    Task<IEnumerable<Category>> GetPaginatedAsync(
        CategoryParameters categoryParameters,
        string? search = null,
        object[]? searchParameters = null
    );
}