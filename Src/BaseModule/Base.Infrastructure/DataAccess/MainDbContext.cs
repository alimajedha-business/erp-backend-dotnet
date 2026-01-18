using Microsoft.EntityFrameworkCore;
using NGErp.HCM.Domain.Entities;
using NGErp.Warehouse.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContext : ApplicationContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options, typeof(Company),
            [typeof(Department), typeof(Category)])
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CI_AI");
            base.OnModelCreating(modelBuilder);
        }

        #region General
        public virtual DbSet<Company> Companies { get; set; }
        #endregion

        #region HCM
        public virtual DbSet<Department> Departments { get; set; }
        #endregion

        #region Warehouse
        public virtual DbSet<Warehouse.Domain.Entities.Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeEnumValue> AttributeEnumValues { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryAttributeRule> CategoryAttributeRules { get; set; }
        public virtual DbSet<InventoryLot> InventoryLots { get; set; }
        public virtual DbSet<InventoryLotValue> InventoryLotValues { get; set; }
        public virtual DbSet<InventoryMovement> InventoryMovements { get; set; }
        public virtual DbSet<InventoryMovementType> InventoryMovementTypes { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemAttributeValue> ItemAttributeValues { get; set; }
        public virtual DbSet<ItemUom> ItemUoms { get; set; }
        public virtual DbSet<ItemUomConversion> ItemUomConversions { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public virtual DbSet<Warehouse.Domain.Entities.Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseLocation> WarehouseLocations { get; set; }
        #endregion
    }

}
