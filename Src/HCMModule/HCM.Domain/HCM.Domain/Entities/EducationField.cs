using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EducationField : BaseEntity, IBaseEntityTypeConfiguration<EducationField>
{
    public string Title { get; set; } = default!;

    public void Map(EntityTypeBuilder<EducationField> builder)
    {
        builder
            .ToTable(nameof(EducationField), "HCM");

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasIndex(e => e.Title)
            .IsUnique();
    }
}
