using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class ContentType : IViewModelTypeConfiguration<ContentType>
{
    public int Id { get; set; }
    public required string AppLabel { get; set; }
    public required string Model { get; set; }

    public void Map(EntityTypeBuilder<ContentType> builder)
    {
        builder.ToTable(
            "django_content_type",
            "dbo",
            t =>
            {
                t.ExcludeFromMigrations();
            }
        );

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AppLabel).HasColumnName("app_label").HasMaxLength(100);
        builder.Property(e => e.Model).HasColumnName("model").HasMaxLength(100);

        builder.HasIndex(e => new { e.AppLabel, e.Model }).IsUnique();
    }
}
