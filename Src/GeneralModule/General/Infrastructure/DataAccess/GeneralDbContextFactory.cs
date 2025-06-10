using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess
{
    public class GeneralDbContextFactory : IDesignTimeDbContextFactory<GeneralDbContext>
    {
        public GeneralDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<GeneralDbContext>()
                .UseSqlServer(configuration.GetConnectionString("NGERPDatabase"));

            return new GeneralDbContext(builder.Options);
        }
    }
}
