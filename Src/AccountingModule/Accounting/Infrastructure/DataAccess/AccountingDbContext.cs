using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using NGErp.Accounting.Infrastructure.DataAccess.Repositories.Configuration;

namespace Accounting.Infrastructure.DataAccess
{
    public class AccountingDbContext : DbContext
    {
        public DbSet<Ledger> Ledgers { get; set; }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LedgerConfiguration());
        }
    }
}