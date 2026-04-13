using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryLevelConstraintRepository(MainDbContext context) :
    RepositoryWithCompany<CategoryLevelConstraint>(context),
    ICategoryLevelConstraintRepository
{
    public async Task<CategoryLevelConstraint?> GetByLevelNoAsync(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    )
    {
        return await _dbSet.SingleOrDefaultAsync(e => e.LevelNo == levelNo, ct);
    }

    public async Task<int> GetNextLevelAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.LevelNo, ct);

        return (maxCode ?? 0) + 1;
    }
}
