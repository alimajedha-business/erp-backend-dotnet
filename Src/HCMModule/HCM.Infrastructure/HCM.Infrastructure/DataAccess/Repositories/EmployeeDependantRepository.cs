using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmployeeDependantRepository(MainDbContext context) :
    Repository<EmployeeDependant>(context),
    IEmployeeDependantRepository
{
    public override async Task<EmployeeDependant?> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.Id == id)
            .Include(e => e.EmployeeRelative)
            .ThenInclude(e => e.Employee)
            .ThenInclude(e => e.Person)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<EmployeeDependant?> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.EmployeeRelative.EmployeeId == employeeId && e.Id == id)
            .Include(e => e.EmployeeRelative)
            .ThenInclude(e => e.Employee)
            .ThenInclude(e => e.Person)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public IQueryable<EmployeeDependant> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Where(e => e.EmployeeRelative.EmployeeId == employeeId)
            .Include(e => e.EmployeeRelative)
            .ThenInclude(e => e.Employee)
            .ThenInclude(e => e.Person)
            .Filter(requestAdvancedFilters);
    }
}
