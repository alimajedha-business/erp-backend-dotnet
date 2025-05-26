using Microsoft.EntityFrameworkCore;
using Accounting.Domain.Entities;
using Accounting.Infrastructure.DataAccess.Repositories.Configuration;

namespace Accounting.Infrastructure.DataAccess
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LedgerConfiguration());
            modelBuilder.Entity<Ledger>().ToTable("Ledgers", "accounting");
        }
        public DbSet<Ledger> Ledgers { get; set; }
    }
}