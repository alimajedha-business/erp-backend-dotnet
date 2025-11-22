using Base.Infrastructure.DataAccess;
using General.Service.Interfaces.Repositories;
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
        public CompanyRepository(MainDbContext dbContext) : base(dbContext) { }

        public async Task<Company?> GetCompanyAsync(int companyId, bool trackChanges) =>
             await FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();

        public async Task<Company?> GetCompanyForDomainAsync(int domainId, int companyId, bool trackChanges) =>
            await FindByCondition(c => c.DomainId.Equals(domainId) && c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();
    }
}
