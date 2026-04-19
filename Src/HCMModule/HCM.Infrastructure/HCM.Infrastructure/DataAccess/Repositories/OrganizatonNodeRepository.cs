using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class OrganizationNodeRepository(MainDbContext context) :
    RepositoryWithCompany<OrganizationNode>(context),
    IOrganizationNodeRepository
{

    public async Task<OrganizationNode?> GetByDepartmentIdAsync(Guid companyid, Guid departemnId, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking().Where(w => w.DepartmentId == departemnId && w.CompanyId == companyid).SingleOrDefaultAsync(ct);
    }
    
    public async Task<OrganizationNode?> GetByPositionIdAsync(Guid companyid, Guid positionId, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking().Where(w => w.PositionId == positionId && w.CompanyId == companyid).SingleOrDefaultAsync(ct);
    }

}