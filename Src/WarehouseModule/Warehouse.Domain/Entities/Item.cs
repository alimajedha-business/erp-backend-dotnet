using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Item :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Item>
{
    public required string Code { get; set; }
    public required string Sku { get; set; }
    public required string Title { get; set; }
    public required bool IsActive { get; set; } = true;
    public required Guid CategoryId { get; set; }

    public required Category Category { get; set; }

    public void Map(EntityTypeBuilder<Item> builder)
    {
        builder.
            ToTable(nameof(Item), "Warehouse");

        builder
            .HasIndex(i => i.CategoryId)
            .HasDatabaseName("IX_Item_Category");

        builder
            .HasIndex(i => new { i.CompanyId, i.Sku })
            .IsUnique()
            .HasDatabaseName("UX_Item_Company_Sku");

        builder
            .Property(e => e.Code)
            .HasMaxLength(80);

        builder
            .Property(e => e.Sku)
            .HasMaxLength(80);

        builder
            .Property(e => e.Title)
            .HasMaxLength(255);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .HasOne(e => e.Category)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
