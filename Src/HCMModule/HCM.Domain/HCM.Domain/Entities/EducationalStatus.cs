using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;
public enum EducationalStatusType 
{
    Student = 1,
    UniversityStudent = 2
}

public class EducationalStatus : BaseEntity, IBaseEntityTypeConfiguration<EducationalStatus>
{
    public string Title { get; set; } = default!;
    public EducationalStatusType Type { get; set; }

    public void Map(EntityTypeBuilder<EducationalStatus> builder)
    {
        builder
            .ToTable(nameof(EducationalStatus), "HCM", t =>
                t.HasCheckConstraint(
                    "CK_EducationalStatus_Type",
                    "[Type] BETWEEN 1 AND 2"
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
