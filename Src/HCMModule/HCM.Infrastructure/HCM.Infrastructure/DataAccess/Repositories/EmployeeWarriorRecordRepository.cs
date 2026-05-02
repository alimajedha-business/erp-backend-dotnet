using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmployeeWarriorRecordRepository(MainDbContext context) :
    Repository<EmployeeWarriorRecord>(context),
    IEmployeeWarriorRecordRepository
{
    public async Task<EmployeeWarriorRecord?> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.EmployeeId == employeeId && e.Id == id)
            .Include(e => e.Employee)
            .ThenInclude(e => e.Person)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public IQueryable<EmployeeWarriorRecord> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Where(e => e.EmployeeId == employeeId)
            .Include(e => e.Employee)
            .ThenInclude(e => e.Person)
            .Filter(requestAdvancedFilters);
    }
}
