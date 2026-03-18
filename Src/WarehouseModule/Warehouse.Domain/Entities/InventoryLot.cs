using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLot :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLot>
{
    public required byte[] DimHash { get; set; }
    public required string Serial { get; set; }
    public required Guid ItemId { get; set; }

    public required Item Item { get; set; }

    public void Map(EntityTypeBuilder<InventoryLot> builder)
    {
        builder
            .ToTable(nameof(InventoryLot), "Warehouse");

        builder
           .HasIndex(i => new { i.ItemId, i.Serial, i.DimHash })
           .IsUnique()
           .HasDatabaseName("UX_InvLot_Item_DimHash");

        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_InventoryLot_Item");

        builder
            .Property(e => e.DimHash)
            .HasColumnType("varbinary(32)")
            .HasMaxLength(32);

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
