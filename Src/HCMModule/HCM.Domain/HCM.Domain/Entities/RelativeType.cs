using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum RelationshipType
{
    Father = 1,
    Mother = 2,
    Brother = 3, 
    Sister = 4,
    spouse = 5
}



public class RelativeType : BaseEntity, IBaseEntityTypeConfiguration<RelativeType>
{
    public string Title { get; set; } = default!;
    public RelationshipType Type { get; set; }

    public void Map(EntityTypeBuilder<RelativeType> builder)
    {
        builder
            .ToTable(nameof(RelativeType), "HCM", t =>
                t.HasCheckConstraint(
                    "CK_RelativeType_Type",
                    "[Type] BETWEEN 1 AND 5"
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
