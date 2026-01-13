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

    public static void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Warehouse_Company_Code");
    }

    public void Map(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .ToTable(nameof(Warehouse), "Warehouse");
    }
}
