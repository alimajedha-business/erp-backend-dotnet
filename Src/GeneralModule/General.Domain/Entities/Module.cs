using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class Module : IViewModelTypeConfiguration<Module>
{
    public long Id { get; set; }
    public required string NameFa { get; set; }
    public required string NameEn { get; set; }
    public required string Color { get; set; }
    public required string Prefix { get; set; }

    public ICollection<EntityType> EntityTypes { get; set; } = [];

    public void Map(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("modules", "general", t => t.ExcludeFromMigrations());

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.NameFa).HasColumnName("name_fa").HasMaxLength(50);
        builder.Property(e => e.NameEn).HasColumnName("name_en").HasMaxLength(50);
        builder.Property(e => e.Color).HasColumnName("color").HasMaxLength(6);
        builder.Property(e => e.Prefix).HasColumnName("prefix").HasMaxLength(40);

        builder.HasIndex(e => e.NameFa).IsUnique();
        builder.HasIndex(e => e.NameEn).IsUnique();
    }
}
