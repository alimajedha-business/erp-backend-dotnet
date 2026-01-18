using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var builder = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlServer("Server=ALIMAJEDHA\\MSSQLSERVER2022;Database=NGERP;User Id=AliMajedHA;Password=AliMajedHA19Dec1992;Encrypt=False;");

            return new MainDbContext(builder.Options);
        }
    }
}
