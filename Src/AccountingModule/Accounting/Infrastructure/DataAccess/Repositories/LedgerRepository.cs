using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Accounting.Application.Interfaces.Repositories;
using Common.Infrastructure.DataAccess;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public sealed class LedgerRepository : RepositoryBase<Ledger>, ILedgerRepository
    {
        public LedgerRepository(AccountingDbContext context) : base(context)
        {
        }
        public IEnumerable<Ledger> GetAll(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.Id)
            .ToList();
        public Ledger? Get(int LedgerId, bool trackChanges) =>
            FindByCondition(l => l.Id.Equals(LedgerId), trackChanges)
            .SingleOrDefault();
        public new void Create(Ledger ledger)
        {
            ledger.CreatedAt = DateTime.Now;
            base.Create(ledger);
        }

        public IEnumerable<Ledger> GetByIds(IEnumerable<int> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public new void Delete(Ledger ledger) => base.Delete(ledger);
    }
}