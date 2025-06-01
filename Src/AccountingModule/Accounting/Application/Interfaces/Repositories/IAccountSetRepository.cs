using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Interfaces.Repositories
{
    public interface IAccountSetRepository
    {
        IEnumerable<AccountSet> GetAll(int ledgerId, bool trackChanges);
        AccountSet? Get(int ledgerId, int accountSetId, bool trackChanges);
        void Create(int companyId, int ledgerId, AccountSet accountSet);
        void Delete(AccountSet accountSet);
    }
}
