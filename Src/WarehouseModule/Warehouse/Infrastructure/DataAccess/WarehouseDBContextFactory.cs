using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.DataAccess
{
    public class WarehouseDbContextFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
    {
        public WarehouseDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var builder = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseSqlServer("Server=.\\sql2022;Database=NGERP;User Id=sa;Password=123;Encrypt=False;");

            return new WarehouseDbContext(builder.Options);
        }
    }
}
