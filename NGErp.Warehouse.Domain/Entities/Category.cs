using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class Category :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Category>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public int LevelNo { get; private set; }
    public bool IsLastLevel { get; private set; }
    public string CategoryPath { get; private set; } = default!;

    [ForeignKey(nameof(Category))]
    public Guid ParentCategoryId { get; private set; }

    public static void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(i => new { i.Code, i.CompanyId })
            .IsUnique()
            .HasDatabaseName("UX_Attribute_Company_Code");
    }

    public void Map(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable(nameof(Category), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_Category_LevelNo",
                "LevelNo BETWEEN 1 AND 7"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_Category_LevelNo_LastLevel",
                "(LevelNo = 1 AND IsLastLevel = 0) OR LevelNo <> 1"
            ));

        builder
            .Property(e => e.Code)
            .HasMaxLength(64);

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.CategoryPath)
            .HasMaxLength(1024);
    }
}
