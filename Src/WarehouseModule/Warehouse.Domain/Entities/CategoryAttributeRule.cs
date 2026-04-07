using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class CategoryAttributeRule :
    BaseEntity,
    IBaseEntityTypeConfiguration<CategoryAttributeRule>
{
    public Guid CategoryId { get; set; }
    public Guid AttributeId { get; set; }
    public bool IsItemAttribute { get; set; } = false;
    public bool IsRequired { get; set; } = false;
    public bool IsStockDimension { get; set; } = false;
    public int? SortOrder { get; set; }

    public Category Category { get; set; } = default!;
    public Attribute Attribute { get; set; } = default!;

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
            .OnDelete(DeleteBehavior.NoAction);
    }
}
