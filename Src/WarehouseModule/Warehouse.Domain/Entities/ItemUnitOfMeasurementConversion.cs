using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUnitOfMeasurementConversion :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUnitOfMeasurementConversion>
{
    public decimal Factor { get; private set; }
    public Guid ItemId { get; private set; }
    public Guid FromUnitOfMeasurementId { get; private set; }
    public Guid ToUnitOfMeasurementId { get; private set; }

    public required Item Item { get; set; }
    public required UnitOfMeasurement FromUnitOfMeasurement { get; set; }
    public required UnitOfMeasurement ToUnitOfMeasurement { get; set; }

    public void Map(EntityTypeBuilder<ItemUnitOfMeasurementConversion> builder)
    {
        builder
            .ToTable(nameof(ItemUnitOfMeasurementConversion), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_ItemUomConv_Factor",
                "Factor > 0"
            ));

        builder
            .HasIndex(i => new { i.ItemId, i.FromUnitOfMeasurementId, i.ToUnitOfMeasurementId })
            .IsUnique()
            .HasDatabaseName("UX_ItemUomConv_Unique");

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.FromUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.FromUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.ToUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.ToUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
