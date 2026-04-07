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

    public Item Item { get; set; } = default!;
    public UnitOfMeasurement UnitOfMeasurement { get; set; } = default!;

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurement), "Warehouse")
            .HasKey(k => new { k.ItemId, k.UnitOfMeasurementId });

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.ItemUnitOfMeasurements)
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.UnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
