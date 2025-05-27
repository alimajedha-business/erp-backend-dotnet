using Accounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Interfaces
{
    public interface IAccountSetRepository
    {
        IEnumerable<AccountSet> GetAllAccountSets(int ledgerId, bool trackChanges);
        AccountSet? GetAccountSet(int AccountSetId, bool trackChanges);
    }
}
