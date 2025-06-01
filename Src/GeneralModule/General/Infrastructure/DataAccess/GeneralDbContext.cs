using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.DataAccess
{
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().ToTable("currencies", "general");
            modelBuilder.Entity<Country>().ToTable("countries", "general");
            modelBuilder.Entity<Province>().ToTable("provinces", "general");
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
    }
}
