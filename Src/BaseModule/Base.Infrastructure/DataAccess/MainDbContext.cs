using Microsoft.EntityFrameworkCore;

using NGErp.HCM.Domain.Entities;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContext : ApplicationContext
    {    
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options,typeof(Department))
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

        #region Warehouse
        public virtual DbSet<Warehouse.Domain.Entities.Attribute> Attribute { get; set; }
        public virtual DbSet<AttributeEnumValue> AttributeEnumValue { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryAttributeRule> CategoryAttributeRule { get; set; }
        public virtual DbSet<InventoryLot> InventoryLot { get; set; }
        public virtual DbSet<InventoryLotValue> InventoryLotValue { get; set; }
        public virtual DbSet<InventoryMovement> InventoryMovement { get; set; }
        public virtual DbSet<InventoryMovementType> InventoryMovementType { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemAttributeValue> ItemAttributeValue { get; set; }
        public virtual DbSet<ItemUom> ItemUom { get; set; }
        public virtual DbSet<ItemUomConversion> ItemUomConversion { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public virtual DbSet<Warehouse.Domain.Entities.Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseLocation> WarehouseLocation { get; set; }
        #endregion
    }

}
