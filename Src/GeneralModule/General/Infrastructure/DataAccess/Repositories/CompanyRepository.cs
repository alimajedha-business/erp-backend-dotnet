using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GeneralDbContext dbContext) : base(dbContext) { }

        public async Task<Company?> GetCompanyAsync(int domainId, int companyId, bool trackChanges) =>
         await FindByCondition(d => d.DomainId.Equals(domainId) && d.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();
    }
}
