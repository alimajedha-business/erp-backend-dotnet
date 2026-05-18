using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLot :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLot>
{
    public Guid ItemId { get; set; }

    // Hash of stock-dimension attribute values:
    // expiry date, batch number, serial, color, size, etc.
    public required byte[] StockKeyHash { get; set; }

    public Item Item { get; set; } = default!;

    public ICollection<InventoryLotAttributeValue> AttributeValues { get; set; } = [];

    public void Map(EntityTypeBuilder<InventoryLot> builder)
    {
        builder
            .ToTable(nameof(InventoryLot), "Warehouse");

        builder
           .HasIndex(i => new { i.ItemId, i.StockKeyHash })
           .IsUnique()
           .HasDatabaseName("UX_InvLot_Item_DimHash");

        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_InventoryLot_Item");

        builder
            .Property(e => e.StockKeyHash)
            .HasMaxLength(32);

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}