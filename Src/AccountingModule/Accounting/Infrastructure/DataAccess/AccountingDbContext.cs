using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;

namespace Accounting.Infrastructure.DataAccess
{
    public class AccountingDbContext : DbContext
    {
        public DbSet<Ledger> Ledgers { get; set; }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
            : base(options) { }
    }
}