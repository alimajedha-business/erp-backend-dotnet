using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemWarehouse :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemWarehouse>
{
    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal CriticalPoint { get; set; }
    public decimal ReorderQuantity { get; set; }
    public decimal MaxStockLevel { get; set; }

    public Item Item { get; set; } = default!;
    public Warehouse Warehouse { get; set; } = default!;

    public ICollection<ItemWarehouseLocation> ItemWarehouseLocations { get; set; } = [];

    public void Map(EntityTypeBuilder<ItemWarehouse> builder)
    {
        builder
            .ToTable(nameof(ItemWarehouse), "Warehouse")
            .HasKey(k => new { k.ItemId, k.WarehouseId });

        builder
            .Property(e => e.ReorderPoint)
            .HasPrecision(22, 4);

        builder
            .Property(e => e.CriticalPoint)
            .HasPrecision(22, 4);

        builder
            .Property(e => e.ReorderQuantity)
            .HasPrecision(22, 4);

        builder
            .Property(e => e.MaxStockLevel)
            .HasPrecision(22, 4);

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.ItemWarehouses)
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.Warehouse)
            .WithMany(e => e.ItemWarehouses)
            .HasForeignKey(e => e.WarehouseId);
    }
}
