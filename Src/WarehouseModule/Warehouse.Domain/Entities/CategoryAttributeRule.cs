using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class CategoryAttributeRule :
    BaseEntity,
    IBaseEntityTypeConfiguration<CategoryAttributeRule>
{
    public Guid CategoryId { get; private set; }
    public Guid AttributeId { get; private set; }
    public bool IsItemAttribute { get; private set; } = false;
    public bool IsRequired { get; private set; } = false;
    public bool IsStockDimension { get; private set; } = false;
    public int SortOrder { get; private set; }

    public required Category Category { get; set; }
    public required Attribute Attribute { get; set; }

    public void Map(EntityTypeBuilder<CategoryAttributeRule> builder)
    {
        builder
            .ToTable(nameof(CategoryAttributeRule), "Warehouse");

        builder
            .HasIndex(i => new { i.CategoryId, i.AttributeId })
            .IsUnique()
            .HasDatabaseName("UX_CategoryAttribute_Category_Attribute");

        builder
            .Property(e => e.IsItemAttribute)
            .HasDefaultValue(false);

        builder
            .Property (e => e.IsRequired)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsStockDimension)
            .HasDefaultValue(false);

        builder
            .HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
