using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class PositionJobRepository(MainDbContext context) :
    RepositoryWithCompany<PositionJob>(context),
    IPositionJobRepository
{
    public async Task<PositionJob?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return await query
            .FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId, ct);
    }
}
