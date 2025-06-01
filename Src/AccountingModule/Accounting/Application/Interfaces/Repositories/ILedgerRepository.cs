using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Interfaces.Repositories
{
    public interface ILedgerRepository
    {
        IEnumerable<Ledger> GetAll(bool trackChanges);
        Ledger? Get(int LedgerId, bool trackChanges);
        void Create(Ledger ledger);
        IEnumerable<Ledger> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void Delete(Ledger ledger);
    }
}