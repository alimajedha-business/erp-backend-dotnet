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

        public DbSet<WarehouseType> WarehouseTypes { get; set; }

       // public DbSet<WarehouseStock> WarehouseStocks { get; set; }

        //  public DbSet<ProductHierarchy> ProductHierarchies { get; set; }
         //  public DbSet<ProductCode> ProductCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
