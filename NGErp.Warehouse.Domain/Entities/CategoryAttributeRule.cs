using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class CategoryAttributeRule : BaseEntity, IBaseEntityTypeConfiguration<CategoryAttributeRule>
{
    public void Map(EntityTypeBuilder<CategoryAttributeRule> builder)
    {
        builder.ToTable(nameof(CategoryAttributeRule), "Warehouse");
    }
}
