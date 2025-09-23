using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Interfaces.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetAllAsync(bool trackChanges);
        Task<Ledger?> GetAsync(int LedgerId, bool trackChanges);
        void Create(Ledger ledger);
        Task<IEnumerable<Ledger>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void Delete(Ledger ledger);
    }
}