using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Domain.Interfaces
{
    public interface ILedgerRepository
    {
        IEnumerable<Ledger> GetAllLedgers(bool trackChanges);
        Ledger? GetLedger(int LedgerId, bool trackChanges);
        void CreateLedger(Ledger ledger);
        IEnumerable<Ledger> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void DeleteLedger(Ledger ledger);
    }
}