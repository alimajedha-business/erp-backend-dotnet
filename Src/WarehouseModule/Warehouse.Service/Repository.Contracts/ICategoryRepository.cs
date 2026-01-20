using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
}