using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using NGErp.Base.Service.Services;


namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContextFactory : 
        IDesignTimeDbContextFactory<MainDbContext>
    {

        public MainDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var builder = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlServer("Server=ALIMAJEDHA\\MSSQLSERVER2022;Database=NGERP;User Id=AliMajedHA;Password=AliMajedHA19Dec1992;Encrypt=False;");

            return new MainDbContext(builder.Options, new DesignTimeCurrentUserService());
        }

        private sealed class DesignTimeCurrentUserService : ICurrentUserService
        {
            public bool IsAuthenticated => false;
            public string? Email => null;
            public string? Token => null;
            public string? UserId => null;
            public string? Username => null;
            public ClaimsPrincipal? User => null;
        }
    }
}
