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
    public Guid WarehouseId { get; set; }
    public bool CanStoreItem { get; set; } = false;
    public int LevelNo { get; set; }
    public bool HasNextLevel { get; set; } = false;

    public WarehouseLocation? ParentLocation { get; set; }
    public Warehouse Warehouse { get; set; } = default!;

    public ICollection<WarehouseLocation> SubLocations { get; set; } = [];

    public void Map(EntityTypeBuilder<WarehouseLocation> builder)
    {
        builder
            .ToTable(nameof(WarehouseLocation), "Warehouse");

        builder
            .HasIndex(i => new { i.WarehouseId, i.ParentLocationId })
            .HasDatabaseName("IX_Location_Warehouse_Parent");

        builder
            .HasIndex(i => new { i.WarehouseId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_WarehouseLocation_Warehouse_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

        builder
            .Property(e => e.CanStoreItem)
            .HasDefaultValue(false);

        builder
            .Property(e => e.HasNextLevel)
            .HasDefaultValue(false);

        builder
            .HasOne(e => e.ParentLocation)
            .WithMany(e => e.SubLocations)
            .HasForeignKey(e => e.ParentLocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Warehouse)
            .WithMany()
            .HasForeignKey(e => e.WarehouseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
