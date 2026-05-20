using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceType :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceType>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required bool AddToStock { get; set; }

    public RemittanceTypeConfiguration RemittanceTypeConfiguration { get; set; } = null!;

    public void Map(EntityTypeBuilder<RemittanceType> builder)
    {
        builder
            .ToTable(nameof(RemittanceType), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_RemittanceType_Company_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.AddToStock)
            .IsRequired();
    }
}
