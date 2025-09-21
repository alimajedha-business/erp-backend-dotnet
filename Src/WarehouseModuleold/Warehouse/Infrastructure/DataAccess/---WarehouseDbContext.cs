using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;


namespace Warehouse.Infrastructure.DataAccess
{
    public partial class WarehouseDbContext : DbContext
    {
        
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

       // public virtual DbSet<WarehouseType>? WarehouseTypes { get; set; }
  

        public DbSet<WarehouseStock> WarehouseStocks { get; set; }

       // public virtual DbSet<ProductCode> ProductCodes { get; set; }

       // public virtual DbSet<ProductHierarchy> ProductHierarchies { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.UseCollation("Persian_100_CI_AI");

        //    //modelBuilder.Entity<WarehouseType>(entity =>
        //    //{
        //    //    entity.HasKey(e => e.Id).HasName("PK__warehouse___3213E83F523151EB");
        //    //});

        //    modelBuilder.Entity<WarehouseStock>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
                         
        //    });

        

        //}

    }
}
