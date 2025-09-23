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
        Task<IEnumerable<AccountSet>> GetAllAsync(int ledgerId, bool trackChanges);
        Task<AccountSet?> GetAsync(int ledgerId, int accountSetId, bool trackChanges);
        void Create(int companyId, int ledgerId, AccountSet accountSet);
        void Delete(AccountSet accountSet);
    }
}
