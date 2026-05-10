using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurement :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurement>
{
    public Guid ItemId { get; set; }
    public Guid UnitOfMeasurementId { get; set; }
    public required int UnitOrder { get; set; }

    public decimal? Weigh { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? CubeVolume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredLengthUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }

    public Item Item { get; set; } = default!;
    public UnitOfMeasurement UnitOfMeasurement { get; set; } = default!;
    public Unit? PreferredMassUnit { get; set; }
    public Unit? PreferredLengthUnit { get; set; }
    public Unit? PreferredVolumeUnit { get; set; }

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurement), "Warehouse")
            .HasKey(k => new { k.ItemId, k.UnitOfMeasurementId });

        builder
            .Property(e => e.Weigh)
            .HasPrecision(28, 14);

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
            .Property(e => e.CubeVolume)
            .HasPrecision(28, 14);

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.ItemUnitOfMeasurements)
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.UnitOfMeasurement)
            .WithMany(e => e.ItemUnitOfMeasurements)
            .HasForeignKey(e => e.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Cascade);

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
