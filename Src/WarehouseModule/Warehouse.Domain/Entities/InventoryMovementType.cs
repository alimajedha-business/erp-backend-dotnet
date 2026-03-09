using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovementType :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryMovementType>
{
    public int Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;

    public void Map(EntityTypeBuilder<InventoryMovementType> builder)
    {
        builder
            .ToTable(nameof(InventoryMovementType), "Warehouse");

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);
    }
}
