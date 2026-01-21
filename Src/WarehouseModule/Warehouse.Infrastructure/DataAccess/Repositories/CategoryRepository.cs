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
    public async Task<IEnumerable<Category>> GetPaginatedCategoriesAsync(CategoryParameters prms)
    {
        return await GetPaginatedAsync(prms)
            .Where(e => e.CompanyId == new Guid("6f7be93f-c740-43b1-96b1-c6e3ff3af4ef"))
            .ToListAsync();
    }
}
