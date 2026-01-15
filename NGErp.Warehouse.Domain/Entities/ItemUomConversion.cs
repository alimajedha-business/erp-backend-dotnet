using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUomConversion :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUomConversion>
{
    public decimal Factor { get; private set; }

    public Guid ItemId { get; private set; }
    public required Item Item { get; set; }

    public Guid FromUomId { get; private set; }
    public required UnitOfMeasurement FromUom { get; set; }

    public Guid ToUomId { get; private set; }
    public required UnitOfMeasurement ToUom { get; set; }

    public void Map(EntityTypeBuilder<ItemUomConversion> builder)
    {
        builder
            .ToTable(nameof(ItemUomConversion), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_ItemUomConv_Factor",
                "Factor > 0"
            ));

        builder
            .HasIndex(i => new { i.ItemId, i.FromUomId, i.ToUomId })
            .IsUnique()
            .HasDatabaseName("UX_ItemUomConv_Unique");

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.FromUom)
            .WithMany()
            .HasForeignKey(e => e.FromUomId);

        builder
            .HasOne(e => e.ToUom)
            .WithMany()
            .HasForeignKey(e => e.ToUomId);
    }
}
