using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovementType :
    BaseEntity,
    IBaseEntityTypeConfiguration<InventoryMovementType>
{
    public string Title { get; private set; } = default!;

    public void Map(EntityTypeBuilder<InventoryMovementType> builder)
    {
        builder
            .ToTable(nameof(InventoryMovementType), "Warehouse");

        builder
            .Property(e => e.Title)
            .HasMaxLength(20);
    }
}
