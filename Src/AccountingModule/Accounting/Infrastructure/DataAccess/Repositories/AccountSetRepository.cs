using Accounting.Application.Interfaces.Repositories;
using Accounting.Domain.Entities;
using Common.Application.Interfaces;
using Common.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public class AccountSetRepository : RepositoryBase<AccountSet>, IAccountSetRepository
    {
        public AccountSetRepository(IMainDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccountSet>> GetAllAsync(int ledgerId, bool trackChanges) =>
          await FindByCondition(a => a.LedgerId.Equals(ledgerId), trackChanges)
            .OrderBy(a => a.Title)
            .ToListAsync();

        public async Task<AccountSet?> GetAsync(int ledgerId, int accountSetId, bool trackChanges) =>
           await FindByCondition(a => a.LedgerId.Equals(ledgerId) && a.Id.Equals(accountSetId), trackChanges)
            .SingleOrDefaultAsync();

        public void Create(int companyId, int ledgerId, AccountSet accountSet)
        {
            accountSet.CompanyId = companyId;
            accountSet.LedgerId = ledgerId;
            Create(accountSet);
        }

        public new void Delete(AccountSet accountSet) => base.Delete(accountSet);

    }
}
