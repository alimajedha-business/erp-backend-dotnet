using Accounting.Domain.Entities;

namespace Accounting.Domain.Interfaces
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetByDateRangeAsync(DateTime start, DateTime end);
    }
}