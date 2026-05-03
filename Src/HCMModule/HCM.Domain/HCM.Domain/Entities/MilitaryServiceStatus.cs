using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum MilitaryStatusType
{
    NotInclude = 1,
    Included = 2,
    Exempt = 3,
    Completed = 4,
    StudentExeption = 5,
    Absent = 6,
    InProgress = 7,
}

public class MilitaryServiceStatus : BaseEntity, IBaseEntityTypeConfiguration<MilitaryServiceStatus>
{
    public string Title { get; set; } = default!;
    public MilitaryStatusType Type { get; set; }

    public void Map(EntityTypeBuilder<MilitaryServiceStatus> builder)
    {
        builder
            .ToTable("MilitaryServiceStatus", "HCM", t =>
                t.HasCheckConstraint(
                    "CK_MilitaryServiceStatus_Type",
                    "[Type] BETWEEN 1 AND 7"
                ));

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.Title)
            .IsUnique();

        builder
            .Property(e => e.Type)
            .IsRequired();
    }
}
