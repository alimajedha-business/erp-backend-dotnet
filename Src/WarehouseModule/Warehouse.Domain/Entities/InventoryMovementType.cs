using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovementType :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryMovementType>
{
    public required int Code { get; set; }
    public required string Title { get; set; }

    public void Map(EntityTypeBuilder<InventoryMovementType> builder)
    {
        builder
            .ToTable(nameof(InventoryMovementType), "Warehouse");

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);
    }
}
