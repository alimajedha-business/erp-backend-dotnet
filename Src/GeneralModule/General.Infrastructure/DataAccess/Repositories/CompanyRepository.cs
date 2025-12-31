using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Infrastructure.DataAccess.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(MainDbContext dbContext) : base(dbContext) { }

        public async Task<Company?> GetCompanyAsync(Guid companyId) =>
             await GetByIdAsync(companyId);

        public async Task<Company?> GetCompanyForDomainAsync(Guid domainId, Guid companyId) =>
            await FirstOrDefaultAsync(c => c.DomainId.Equals(domainId) && c.Id.Equals(companyId));
    }
}
