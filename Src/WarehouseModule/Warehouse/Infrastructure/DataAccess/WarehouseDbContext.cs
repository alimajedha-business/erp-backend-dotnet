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
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Warehouse.Domain.Entities.Warehouse> Warehouses { get; set; }        


    }
}
