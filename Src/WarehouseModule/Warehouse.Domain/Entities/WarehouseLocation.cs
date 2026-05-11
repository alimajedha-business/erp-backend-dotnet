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
    public Guid? ParentLocationId { get; set; } = Guid.Empty;
    public Guid WarehouseId { get; set; }
    public bool CanStoreItem { get; set; } = false;
    public int LevelNo { get; set; }
    public bool HasNextLevel { get; set; } = false;
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? MaxMass { get; set; }
    public decimal? MaxVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }

    public WarehouseLocation? ParentLocation { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    public Unit? PreferredMassUnit { get; set; }
    public Unit? PreferredLengthUnit { get; set; }
    public Unit? PreferredVolumeUnit { get; set; }

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
            .Property(e => e.Length)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Width)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Height)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.MaxMass)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.MaxVolume)
            .HasPrecision(28, 14);

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

        builder
            .HasOne(e => e.PreferredMassUnit)
            .WithMany()
            .HasForeignKey(e => e.PreferredMassUnitId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.PreferredLengthUnit)
            .WithMany()
            .HasForeignKey(e => e.PreferredLengthUnitId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.PreferredVolumeUnit)
            .WithMany()
            .HasForeignKey(e => e.PreferredVolumeUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
