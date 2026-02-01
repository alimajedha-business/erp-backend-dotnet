using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseLocation :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<WarehouseLocation>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public Guid? ParentLocationId { get; private set; }
    public Guid WarehouseId { get; private set; }

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
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

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
