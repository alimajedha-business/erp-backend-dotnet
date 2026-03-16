using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseLocation :
    BaseEntity,
    IBaseEntityTypeConfiguration<WarehouseLocation>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public Guid? ParentLocationId { get; set; }
    public required Guid WarehouseId { get; set; }
    public required bool CanStoreItem { get; set; } = false;

    public WarehouseLocation? ParentLocation { get; set; }
    public required Warehouse Warehouse { get; set; }
    public virtual List<WarehouseLocation> SubLocations { get; set; } = [];
    public virtual List<InventoryMovement> SrcLocations { get; set; } = [];
    public virtual List<InventoryMovement> DstLocations { get; set; } = [];

    public void Map(EntityTypeBuilder<WarehouseLocation> builder)
    {
        builder
            .ToTable(nameof(WarehouseLocation), "Warehouse");

        builder
            .HasIndex(i => new { i.WarehouseId, i.ParentLocationId })
            .HasDatabaseName("IX_Location_Warehouse_Parent");

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

        builder
            .Property(e => e.CanStoreItem)
            .HasDefaultValue(false);

        builder
            .HasOne(e => e.ParentLocation)
            .WithMany(e => e.SubLocations)
            .HasForeignKey(e => e.ParentLocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Warehouse)
            .WithMany(e => e.Locations)
            .HasForeignKey(e => e.WarehouseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
