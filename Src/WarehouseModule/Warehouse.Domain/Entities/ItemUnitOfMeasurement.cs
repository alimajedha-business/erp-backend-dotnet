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

    public ICollection<ReceiptLineMeasurementValue> ReceiptLineMeasurementValues { get; set; } = [];

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurement), "Warehouse");

        builder
            .HasIndex(i => new { i.ItemId, i.UnitOfMeasurementId })
            .IsUnique()
            .HasDatabaseName("UX_ItemUnitOfMeasurement_Item_Uom");

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
    }
}
