using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Domain.Interfaces
{
    public interface ILedgerRepository
    {
        IEnumerable<Ledger> GetAllLedgers(bool trackChanges);
        Ledger? GetLedger(int LedgerId, bool trackChanges);
    }
}