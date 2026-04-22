using System.Linq.Expressions;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmploymentGroupRepository(MainDbContext context) :
    RepositoryWithCompany<EmploymentGroup>(context),
    IEmploymentGroupRepository
{
    public async Task<EmploymentGroup?> GetWithSpecificationsAsync(Guid companyId, Guid id, CancellationToken ct)
    {
        var entity = await _context.EmploymentGroups
            .Include(x => x.Specifications)
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Id == id, ct);

        if (entity == null) return null;

        entity.Specifications = entity.Specifications
            .OrderBy(s => s.ValidFrom)
            .ToList();

        return entity;
    }
}