using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Accounting.Infrastructure.DataAccess;

namespace API
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<AccountingDbContext>
    {
        public AccountingDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<AccountingDbContext>()
            .UseSqlServer(configuration.GetConnectionString("NGERPDatabase"));
            return new AccountingDbContext(builder.Options);
        }
    }
}
