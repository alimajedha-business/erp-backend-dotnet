using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ReceiptTypeConfiguration :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptTypeConfiguration>
{
    public Guid ReceiptTypeId { get; set; }

    public ReceiptType ReceiptType { get; set; } = null!;

    public ICollection<ReceiptTypeFieldConfiguration> FieldConfigurations { get; set; } = [];

    public void Map(EntityTypeBuilder<ReceiptTypeConfiguration> builder)
    {
        builder
            .ToTable(nameof(ReceiptTypeConfiguration), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.ReceiptTypeId })
            .IsUnique()
            .HasDatabaseName("UX_ReceiptTypeConfiguration_Company_ReceiptType");

        builder.HasOne(e => e.ReceiptType)
            .WithOne(e => e.ReceiptTypeConfiguration)
            .HasForeignKey<ReceiptTypeConfiguration>(e => e.ReceiptTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
