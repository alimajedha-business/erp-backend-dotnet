using Accounting.Infrastructure.DataAccess;
using General.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Payroll.Infrastructure.DataAccess;
using Shared.Intrastructure.DataAccess;
using Warehouse.Infrastructure.DataAccess;
using Weighing.Infrastructure.DataAccess;

namespace Persistence.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeighingDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneralDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PayrollDbContext).Assembly);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SharedDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }

}
