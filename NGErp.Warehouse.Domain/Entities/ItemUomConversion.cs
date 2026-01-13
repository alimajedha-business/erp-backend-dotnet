using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class ItemUomConversion : BaseEntity, IBaseEntityTypeConfiguration<ItemUomConversion>
{
    public decimal Factor { get; private set; }

    [ForeignKey(nameof(Item))]
    public Guid ItemId { get; private set; }

    [ForeignKey(nameof(Uom))]
    public Guid FromUomId { get; private set; }

    [ForeignKey(nameof(Uom))]
    public Guid ToUomId { get; private set; }

    public static void Configure(EntityTypeBuilder<ItemUomConversion> builder)
    {
        builder
            .HasIndex(i => new { i.ItemId, i.FromUomId, i.ToUomId })
            .IsUnique()
            .HasDatabaseName("UX_ItemUomConv_Unique");
    }

    public void Map(EntityTypeBuilder<ItemUomConversion> builder)
    {
        builder
            .ToTable(nameof(ItemUomConversion), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_ItemUomConv_Factor",
                "Factor > 0"
            ));
    }
}
