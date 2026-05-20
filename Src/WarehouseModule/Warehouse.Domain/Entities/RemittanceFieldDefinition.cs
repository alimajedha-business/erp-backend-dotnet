using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceFieldDefinition :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceFieldDefinition>
{
    public required string Key { get; set; }
    public required string Title { get; set; }
    public required ReceiptFieldDataType DataType { get; set; }
    public required ReceiptFieldPlacement AllowedPlacement { get; set; }
    public bool IsActive { get; set; } = true;

    public void Map(EntityTypeBuilder<RemittanceFieldDefinition> builder)
    {
        builder
            .ToTable(nameof(RemittanceFieldDefinition), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Key })
            .IsUnique();

        builder
            .Property(e => e.Key)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}
