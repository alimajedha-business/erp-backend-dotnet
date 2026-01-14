using NGErp.HCM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NGErp.Base.Domain.Entities;
using System.Reflection;
using NGErp.General.Domain;


namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContext : ApplicationContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options, typeof(Department))
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CI_AI");
            base.OnModelCreating(modelBuilder);
        }

        #region HCM
        public virtual DbSet<Department> Departments { get; set; }
        #endregion
    }

}
