using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurement>
{
    public Guid ItemId { get; private set; }
    public Guid UnitOfMeasurementId { get; private set; }
    public bool IsBase { get; private set; }
    public bool IsDefaultPurchase { get; private set; }
    public bool IsDefalulIssue { get; private set; }

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
            .HasForeignKey(e => e.UnitOfMeasurementId);
    }
}
