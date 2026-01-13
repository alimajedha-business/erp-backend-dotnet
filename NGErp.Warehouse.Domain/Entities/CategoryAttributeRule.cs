using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class CategoryAttributeRule : BaseEntity, IBaseEntityTypeConfiguration<CategoryAttributeRule>
{
    public bool AppliedToDecentents { get; private set; }
    public bool IsRequiredOnMaster { get; private set; }
    public bool IsStockDimension { get; private set; }
    public bool IsRequiredOnMovements { get; private set; }

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public static void Configure(EntityTypeBuilder<CategoryAttributeRule> builder)
    {
        builder
            .HasIndex(i => new { i.AttributeId, i.CompanyId })
            .IsUnique()
            .HasDatabaseName("UX_Attribute_Company_Code");
    }

    public void Map(EntityTypeBuilder<CategoryAttributeRule> builder)
    {
        builder
            .ToTable(nameof(CategoryAttributeRule), "Warehouse");
    }
}
