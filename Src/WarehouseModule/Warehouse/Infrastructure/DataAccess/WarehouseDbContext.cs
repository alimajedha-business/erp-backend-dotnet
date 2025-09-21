using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.DataAccess
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductCode> ProductCodes { get; set; }
        public DbSet<ProductHierarchy> ProductHierarchies { get; set; }
        public DbSet<WarehouseType> WarehouseTypes { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure WarehouseStock entity
            //modelBuilder.Entity<WarehouseStock>()
            //    .ToTable("WarehouseStocks", "Warehouse")
            //    .HasKey(ws => ws.Id);

   
            // Configure foreign key relationship to Company
            //modelBuilder.Entity<WarehouseStock>()
            //    .HasOne<Company>()
            //    .WithMany()
            //    .HasForeignKey(ws => ws.CompanyId)
            //    .OnDelete(DeleteBehavior.Restrict);          

            // Configure other WarehouseStock properties
            //modelBuilder.Entity<WarehouseStock>()
            //    .Property(ws => ws.MaxAssetValue)
            //    .HasColumnType("decimal(18,0)");
        }


    }
}
