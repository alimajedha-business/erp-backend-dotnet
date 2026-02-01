using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories
{
    public class OrganizationalStructureRepository(MainDbContext context) : Repository<OrganizationalStructure>(context), IOrganizationalStructure
    {

    }
}
