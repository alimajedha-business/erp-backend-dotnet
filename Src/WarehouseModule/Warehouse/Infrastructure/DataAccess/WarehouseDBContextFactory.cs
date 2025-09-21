using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            var builder= new DbContextOptionsBuilder<WarehouseDbContext>()
               .UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;Encrypt=False;");              
            return new WarehouseDbContext(builder.Options);
        }
    }
}
