using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface IDomainRepository : IRepository<Domain.Entities.Domain>
    {

    }
}
