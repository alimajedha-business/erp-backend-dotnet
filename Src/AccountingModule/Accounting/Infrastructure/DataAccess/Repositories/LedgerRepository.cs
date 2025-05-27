using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using Common.DataAccess;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public sealed class LedgerRepository : RepositoryBase<Ledger>, ILedgerRepository
    {
        public LedgerRepository(AccountingDbContext context) : base(context)
        {
        }
        public IEnumerable<Ledger> GetAllLedgers(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.Id)
            .ToList();
        public Ledger? GetLedger(int LedgerId, bool trackChanges) =>
            FindByCondition(l => l.Id.Equals(LedgerId), trackChanges)
            .SingleOrDefault();
    }
}