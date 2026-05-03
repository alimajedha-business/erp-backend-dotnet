using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum EducationLevelType
{
    PreDiploma = 1,
    Diploma = 2,
    Associate = 3,
    Bachelor = 4,
    Master = 5,
    PhD = 6,
    Postdoctoral = 7
}

public class EducationLevel : BaseEntity, IBaseEntityTypeConfiguration<EducationLevel>
{
    public string Title { get; set; } = default!;
    public EducationLevelType Type { get; set; }

    public void Map(EntityTypeBuilder<EducationLevel> builder)
    {
        builder
            .ToTable(nameof(EducationLevel), "HCM", t =>
                t.HasCheckConstraint(
                    "CK_EducationLevel_Type",
                    "[Type] BETWEEN 1 AND 7"
                ));

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasIndex(e => e.Title)
            .IsUnique();

        builder
            .Property(e => e.Type)
            .IsRequired();
    }
}
