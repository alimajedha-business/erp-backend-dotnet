using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using Common.DataAccess;
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
        public AccountSetRepository(AccountingDbContext context) : base(context)
        {
        }
        public IEnumerable<AccountSet> GetAllAccountSets(int ledgerId, bool trackChanges) =>
            FindByCondition(a => a.LedgerId.Equals(ledgerId), trackChanges)
            .OrderBy(a => a.Title)
            .ToList();
        public AccountSet? GetAccountSet(int AccountSetId, bool trackChanges) =>
            FindByCondition(a => a.Id.Equals(AccountSetId), trackChanges)
            .SingleOrDefault();
    }
}
