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

        public AccountSet? GetAccountSet(int ledgerId, int accountSetId, bool trackChanges) =>
            FindByCondition(a => a.LedgerId.Equals(ledgerId) && a.Id.Equals(accountSetId), trackChanges)
            .SingleOrDefault();

        public void CreateAccountSetForLedger(int companyId, int ledgerId, AccountSet accountSet)
        {
            accountSet.CompanyId = companyId;
            accountSet.LedgerId = ledgerId;
            Create(accountSet);
        }

        public void DeleteAccountSet(AccountSet accountSet) => Delete(accountSet);

    }
}
