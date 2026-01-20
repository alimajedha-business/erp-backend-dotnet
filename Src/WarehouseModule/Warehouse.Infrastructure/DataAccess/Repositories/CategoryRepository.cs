using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    Repository<Category>(context),
    ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CategoryParameters prms)
    {
        return await GetAllAsync()
            .OrderBy(o => o.Code)
            .Skip(prms.PageSize * (prms.PageNumber - 1))
            .Take(prms.PageSize)
            .ToListAsync();
    }
}
