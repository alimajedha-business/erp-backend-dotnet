using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.DataAccess
{
    public class PayrollDbContextFactory : IDesignTimeDbContextFactory<PayrollDbContext>
    {
        public PayrollDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var builder = new DbContextOptionsBuilder<PayrollDbContext>()
                .UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;");

            return new PayrollDbContext(builder.Options);
        }
    }
}
