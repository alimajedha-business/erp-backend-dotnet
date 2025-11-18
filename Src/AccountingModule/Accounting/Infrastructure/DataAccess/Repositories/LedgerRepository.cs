using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Accounting.Application.Interfaces.Repositories;
using Common.Infrastructure.DataAccess;
using Common.Application.Interfaces;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public sealed class LedgerRepository : RepositoryBase<Ledger>, ILedgerRepository
    {
        public LedgerRepository(IMainDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Ledger>> GetAllAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(l => l.Id)
            .ToListAsync();

        public async Task<Ledger?> GetAsync(int LedgerId, bool trackChanges) =>
          await FindByCondition(l => l.Id.Equals(LedgerId), trackChanges)
            .SingleOrDefaultAsync();

        public new void Create(Ledger ledger)
        {
            ledger.CreatedAt = DateTime.Now;
            base.Create(ledger);
        }

        public async Task<IEnumerable<Ledger>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
           await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public new void Delete(Ledger ledger) => base.Delete(ledger);
    }
}