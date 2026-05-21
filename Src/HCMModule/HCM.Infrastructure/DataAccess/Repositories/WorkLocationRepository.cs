using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class WorkLocationRepository(MainDbContext context) :
    RepositoryWithCompany<WorkLocation>(context),
    IWorkLocationRepository
{
    public async Task<WorkLocation?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.CompanyId == companyId)
            .Where(e => e.Id == id)
            .Include(e => e.Parent)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public override IQueryable<WorkLocation> GetFiltered(
       Guid companyId, RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Include(e => e.Parent)
            .Filter(requestAdvancedFilters);
    }


}
