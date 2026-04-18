using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeRepository(MainDbContext context) :
    RepositoryWithCompany<Domain.Entities.Attribute>(context),
    IAttributeRepository
{
    public async Task<int> GetNextCodeAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }
}
