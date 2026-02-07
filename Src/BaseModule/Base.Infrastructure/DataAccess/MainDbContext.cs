using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using NGErp.Base.Domain.Entities;
using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Services;
using NGErp.General.Domain.Entities;
using NGErp.HCM.Domain.Entities;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContext(
        DbContextOptions<MainDbContext> options,
        ICurrentUserService currentUserService
    ) : ApplicationContext(options, typeof(Company),
        [typeof(Department), typeof(Category)])
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CI_AI");
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;
            var userId = _currentUserService.UserId ?? throw new InvalidUserBadRequestException();

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.ModifierId = Guid.Parse(userId);

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatorId = Guid.Parse(userId);
                    }
                }
            }

            return base.SaveChangesAsync(ct);
        }


        #region General
        public virtual DbSet<Company> Companies { get; set; }
        #endregion

        #region HCM
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<OrganizationalStructure> OrganizationalStructures { get; set; }
        #endregion

        #region Warehouse
        public virtual DbSet<Warehouse.Domain.Entities.Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeEnumValue> AttributeEnumValues { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryAttributeRule> CategoryAttributeRules { get; set; }
        public virtual DbSet<InventoryLot> InventoryLots { get; set; }
        public virtual DbSet<InventoryLotAttributeValue> InventoryLotValues { get; set; }
        public virtual DbSet<InventoryMovement> InventoryMovements { get; set; }
        public virtual DbSet<InventoryMovementType> InventoryMovementTypes { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemAttributeValue> ItemAttributeValues { get; set; }
        public virtual DbSet<ItemUnitOfMeasurement> ItemUnitOfMeasurements { get; set; }
        public virtual DbSet<UnitOfMeasurementConversion> UnitOfMeasurementConversions { get; set; }
        public virtual DbSet<ItemUnitOfMeasurementConversion> ItemUnitOfMeasurementConversions { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public virtual DbSet<Warehouse.Domain.Entities.Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseLocation> WarehouseLocations { get; set; }
        #endregion
    }

}
