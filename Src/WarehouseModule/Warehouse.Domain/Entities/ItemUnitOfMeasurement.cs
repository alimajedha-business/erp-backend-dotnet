using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurement>
{
    public required Guid ItemId { get; set; }
    public required Guid UnitOfMeasurementId { get; set; }
    public required bool IsBase { get; set; } = false;
    public required bool IsDefaultPurchase { get; set; } = false;
    public required bool IsDefaultIssue { get; set; } = false;

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
            .Property(e => e.IsBase)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsDefaultPurchase)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsDefaultIssue)
            .HasDefaultValue(false);

        builder
            .HasOne(e => e.UnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
