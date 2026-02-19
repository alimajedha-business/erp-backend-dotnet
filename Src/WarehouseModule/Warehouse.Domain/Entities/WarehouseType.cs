using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseType :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<WarehouseType>
{
    public string Code { get; private set;  } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public void Map(EntityTypeBuilder<WarehouseType> builder)
    {
        builder
            .ToTable(nameof(WarehouseType), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_WarehouseType_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(false);
    }
}
