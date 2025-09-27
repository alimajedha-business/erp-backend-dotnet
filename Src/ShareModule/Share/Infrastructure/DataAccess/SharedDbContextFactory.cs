using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.DataAccess;
public class SharedDbContextFactory : IDesignTimeDbContextFactory<SharedDbContext>
{
    public SharedDbContext CreateDbContext(string[] args)
    {
        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        var builder = new DbContextOptionsBuilder<SharedDbContext>()
            .UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;");

        return new SharedDbContext(builder.Options);
    }
}
