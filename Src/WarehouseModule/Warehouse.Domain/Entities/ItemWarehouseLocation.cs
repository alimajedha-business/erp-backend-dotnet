using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemWarehouseLocation :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemWarehouseLocation>
{
    public Guid ItemWarehouseId { get; set; }
    public Guid WarehouseLocationId { get; set; }

    public ItemWarehouse ItemWarehouse { get; set; } = default!;
    public WarehouseLocation WarehouseLocation { get; set; } = default!;

    public void Map(EntityTypeBuilder<ItemWarehouseLocation> builder)
    {
        builder
            .ToTable(nameof(ItemWarehouseLocation), "Warehouse");

        builder
            .HasOne(e => e.ItemWarehouse)
            .WithMany(e => e.ItemWarehouseLocations)
            .HasForeignKey(e => e.ItemWarehouseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
