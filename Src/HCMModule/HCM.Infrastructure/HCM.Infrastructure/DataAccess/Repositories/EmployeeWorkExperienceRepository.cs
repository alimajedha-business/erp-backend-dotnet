using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmployeeWorkExperienceRepository(MainDbContext context) :
    Repository<EmployeeWorkExperience>(context),
    IEmployeeWorkExperienceRepository
{

    new public async Task<EmployeeWorkExperience?> GetByIdAsync(
         Guid id,
         bool trackChanges = false,
         CancellationToken ct = default
     )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.Id == id)
            .Include(e => e.Employee).ThenInclude(e => e.Person)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public override IQueryable<EmployeeWorkExperience> GetFiltered(
       RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Include(e => e.Employee).ThenInclude(e => e.Person)
            .Filter(requestAdvancedFilters);
    }

}
