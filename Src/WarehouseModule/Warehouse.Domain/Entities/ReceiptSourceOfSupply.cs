using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ReceiptSourceOfSupply :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptSourceOfSupply>
{
    public int Code { get; set; }
    public string Title { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public void Map(EntityTypeBuilder<ReceiptSourceOfSupply> builder)
    {
        builder
            .ToTable(nameof(ReceiptSourceOfSupply), "Warehouse");

        builder
            .HasIndex(i => new { i.Code })
            .IsUnique()
            .HasDatabaseName("UX_ReceiptSourceOfSupply_Code");

        builder
            .Property(e => e.Code)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);
    }
}
