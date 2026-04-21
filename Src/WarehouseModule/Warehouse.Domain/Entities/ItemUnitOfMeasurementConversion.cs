using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurementConversion :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurementConversion>
{
    public Guid ItemId { get; set; }
    public Guid UnitOfMeasurementId { get; set; }

    public Item Item { get; set; } = default!;
    public UnitOfMeasurement UnitOfMeasurement { get; set; } = default!;

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurementConversion> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurementConversion), "Warehouse");

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.ItemUnitOfMeasurementConversions)
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.UnitOfMeasurement)
            .WithMany(e => e.ItemUnitOfMeasurementConversions)
            .HasForeignKey(e => e.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
