using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class CategoryAttributeRule :
    BaseEntity,
    IBaseEntityTypeConfiguration<CategoryAttributeRule>
{
    public bool AppliedToDecentents { get; private set; }
    public bool IsRequiredOnMaster { get; private set; }
    public bool IsStockDimension { get; private set; }
    public bool IsRequiredOnMovements { get; private set; }
    public int SortOrder { get; private set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; private set; }

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public void Map(EntityTypeBuilder<CategoryAttributeRule> builder)
    {
        builder
            .ToTable(nameof(CategoryAttributeRule), "Warehouse");

        builder
            .HasIndex(i => new { i.CategoryId, i.AttributeId })
            .IsUnique()
            .HasDatabaseName("UX_CategoryAttribute_Category_Attribute");

        builder
            .Property(e => e.AppliedToDecentents)
            .HasDefaultValue(true);

        builder
            .Property(e => e.IsRequiredOnMaster)
            .HasDefaultValue(false);

        builder
            .Property (e => e.IsStockDimension)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsRequiredOnMovements)
            .HasDefaultValue(false);
    }
}
