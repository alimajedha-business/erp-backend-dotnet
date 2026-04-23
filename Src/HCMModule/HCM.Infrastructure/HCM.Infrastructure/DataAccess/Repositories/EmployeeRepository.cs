using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmployeeRepository(MainDbContext context) :
    RepositoryWithCompany<Employee>(context),
    IEmployeeRepository
{
    public async Task<Employee?> GetByIdAsync(
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
            .Include(e => e.MilitaryServiceStatus)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

}