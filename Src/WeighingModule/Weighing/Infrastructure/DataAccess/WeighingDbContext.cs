using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Weighing.Domain.Entities;

namespace Weighing.Infrastructure.DataAccess
{
    public partial class WeighingDbContext : DbContext
    {
        public WeighingDbContext()
        {
        }

        public WeighingDbContext(DbContextOptions<WeighingDbContext> options)
            : base(options)
        {
        }

        public DbSet<DischargeStation> DischargeStations { get; set; } = null!;
        public DbSet<PackageType> PackageTypes { get; set; } = null!;
        public DbSet<PersonDriver> PersonDrivers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeighingDbContext).Assembly);
        }
    }
}
