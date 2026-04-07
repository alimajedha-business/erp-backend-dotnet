using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurement :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurement>
{
    public required Guid ItemId { get; set; }
    public required Guid UnitOfMeasurementId { get; set; }
    public required int UnitOrder { get; set; }

    public required Item Item { get; set; }
    public required UnitOfMeasurement UnitOfMeasurement { get; set; }

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurement), "Warehouse")
            .HasKey(k => new { k.ItemId, k.UnitOfMeasurementId });

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.UnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
