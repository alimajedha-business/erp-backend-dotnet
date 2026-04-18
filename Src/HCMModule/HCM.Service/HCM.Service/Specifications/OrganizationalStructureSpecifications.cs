using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Specifications;

public class XxxOrganizationalStructureSpecification : ISpecification<OrganizationalStructure>
{
    public Func<IQueryable<OrganizationalStructure>, IQueryable<OrganizationalStructure>> Query =>
        query => query.Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Department)
                .Include(s => s.Items!)
                    .ThenInclude(i => i.Node)
                        .ThenInclude(n => n.Position);
}
