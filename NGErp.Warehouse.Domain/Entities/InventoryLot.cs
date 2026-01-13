using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLot : BaseEntity, IBaseEntityTypeConfiguration<InventoryLot>
{
    public byte[] DimHash { get; private set; } = default!;
    public string Serial { get; private set; } = default!;

    [ForeignKey(nameof(Item))]
    public Guid ItemId { get; private set; }

    public static void Configure(EntityTypeBuilder<InventoryLot> builder)
    {
        builder
            .HasIndex(i => new { i.ItemId, i.Serial, i.DimHash })
            .IsUnique()
            .HasDatabaseName("UX_InvLot_Item_DimHash");

        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_InventoryLot_Item");
    }

    public void Map(EntityTypeBuilder<InventoryLot> builder)
    {
        builder
            .ToTable(nameof(InventoryLot), "Warehouse");

        builder
            .Property(e => e.DimHash)
            .HasColumnType("varbinary(32)")
            .HasMaxLength(32);
    }
}
