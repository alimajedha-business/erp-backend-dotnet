using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseType :
    BaseEntity,
    IBaseEntityTypeConfiguration<WarehouseType>
{
    public required int Code { get; set;  }
    public required string Title { get; set; }
    public bool IsActive { get; set; } = true;

    public void Map(EntityTypeBuilder<WarehouseType> builder)
    {
        builder
            .ToTable(nameof(WarehouseType), "Warehouse");

        builder
            .HasIndex(i => new { i.Code })
            .IsUnique()
            .HasDatabaseName("UX_WarehouseType_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}
