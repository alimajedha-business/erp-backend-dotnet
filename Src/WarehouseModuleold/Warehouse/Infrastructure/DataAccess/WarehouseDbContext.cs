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
        public virtual DbSet<WarehouseType> WarehouseTyes { get; set; }
        //public virtual DbSet<WarehouseStock> WarehouseStocks { get; set; }

        public virtual DbSet<ProductHierarchy> ProductHierarchies { get; set; }
    }
}
