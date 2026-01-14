using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Warehouse :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Warehouse>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Type { get; private set; } = default!;

    public virtual List<WarehouseLocation> Locations { get; set; } = [];

    public void Map(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .ToTable(nameof(Warehouse), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Warehouse_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

        builder
            .Property(e => e.Type)
            .HasMaxLength(30);
    }
}
