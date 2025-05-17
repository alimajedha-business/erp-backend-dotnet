using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly AccountingDbContext _context;

        public LedgerRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ledger>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Ledgers
                .Where(l => l.CreatedAt >= start && l.CreatedAt <= end)
                .ToListAsync();
        }
    }
}