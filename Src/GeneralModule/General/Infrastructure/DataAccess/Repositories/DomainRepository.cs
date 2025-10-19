using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public class DomainRepository : RepositoryBase<Domain.Entities.Domain>, IDomainRepository
    {
        public DomainRepository(GeneralDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Domain.Entities.Domain?> GetDomainAsync(int domainId, bool trackChanges) =>
            await FindByCondition(d => d.Id.Equals(domainId), trackChanges).SingleOrDefaultAsync();

    }
}
