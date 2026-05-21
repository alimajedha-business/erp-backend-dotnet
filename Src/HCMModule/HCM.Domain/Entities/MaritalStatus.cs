using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum MaritalStatusType
{
    Single = 1,
    Married = 2,
    HeadOfHouseHold = 3
}

public class MaritalStatus : BaseEntity, IBaseEntityTypeConfiguration<MaritalStatus>
{
    public string Title { get; set; } = default!;
    public MaritalStatusType Type { get; set; }

    public void Map(EntityTypeBuilder<MaritalStatus> builder)
    {
        builder
            .ToTable("MaritalStatus", "HCM", t =>
                t.HasCheckConstraint(
                    "CK_MaritalStatus_Type",
                    "[Type] BETWEEN 1 AND 3"
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
