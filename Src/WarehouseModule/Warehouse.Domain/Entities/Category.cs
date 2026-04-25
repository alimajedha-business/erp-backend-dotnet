using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Category :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Category>
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public int LevelNo { get; set; }
    public bool HasNextLevel { get; set; } = false;
    public Guid? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; private set; }

    public ICollection<Category> SubCategories { get; set; } = [];

    public void Map(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable(nameof(Category), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_Category_LevelNo",
                "LevelNo BETWEEN 1 AND 6"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_Category_LevelNo_HasNextLevel",
                "(LevelNo = 1 AND HasNextLevel = 1) OR (LevelNo = 6 AND HasNextLevel = 0) OR (LevelNo > 1 AND LevelNo < 6 AND HasNextLevel IN (0, 1))"
            ));

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Category_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(64);

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.HasNextLevel)
            .HasDefaultValue(false);

        builder
            .HasOne(e => e.ParentCategory)
            .WithMany(e => e.SubCategories)
            .HasForeignKey(e => e.ParentCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
