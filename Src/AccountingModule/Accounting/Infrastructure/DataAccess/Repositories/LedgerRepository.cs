using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using Common.DataAccess;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public class LedgerRepository : RepositoryBase<Ledger>, ILedgerRepository
    {
        public LedgerRepository(AccountingDbContext context) : base(context)
        {
        }
    }
}