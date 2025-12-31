using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Infrastructure.DataAccess.Repositories
{
    public class DomainRepository : Repository<Domain.Entities.Domain>, IDomainRepository
    {
        public DomainRepository(MainDbContext dbContext) : base(dbContext)
        {
        }


    }
}
