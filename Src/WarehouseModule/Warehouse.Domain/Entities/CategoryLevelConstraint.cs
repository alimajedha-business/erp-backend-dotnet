using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class CategoryLevelConstraint :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<CategoryLevelConstraint>
{
    public required int LevelNo { get; set; }
    public required int CodeLength { get; set; }

    public void Map(EntityTypeBuilder<CategoryLevelConstraint> builder)
    {
        builder
            .ToTable(nameof(CategoryLevelConstraint), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.LevelNo })
            .IsUnique()
            .HasDatabaseName("UX_CategoryLevelConst_Company_No");
    }
}
