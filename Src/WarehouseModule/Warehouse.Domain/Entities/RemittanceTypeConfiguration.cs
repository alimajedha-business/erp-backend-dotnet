using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceTypeConfiguration :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceTypeConfiguration>
{
    public Guid RemittanceTypeId { get; set; }

    public RemittanceType RemittanceType { get; set; } = null!;

    public ICollection<RemittanceTypeFieldConfiguration> FieldConfigurations { get; set; } = [];

    public void Map(EntityTypeBuilder<RemittanceTypeConfiguration> builder)
    {
        builder
            .ToTable(nameof(RemittanceTypeConfiguration), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.RemittanceTypeId })
            .IsUnique()
            .HasDatabaseName("UX_RemittanceTypeConfiguration_Company_RemittanceType");

        builder.HasOne(e => e.RemittanceType)
            .WithOne(e => e.RemittanceTypeConfiguration)
            .HasForeignKey<RemittanceTypeConfiguration>(e => e.RemittanceTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
