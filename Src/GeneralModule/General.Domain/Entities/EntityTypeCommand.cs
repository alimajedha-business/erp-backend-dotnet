using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class EntityTypeCommand : IViewModelTypeConfiguration<EntityTypeCommand>
{
    public Guid Id { get; set; }
    public Guid EntityTypeId { get; set; }
    public required string Key { get; set; }
    public required string NameFa { get; set; }
    public required string NameEn { get; set; }
    public short? Ordering { get; set; }
    public bool Permissible { get; set; }

    public EntityType? EntityType { get; set; }

    public void Map(EntityTypeBuilder<EntityTypeCommand> builder)
    {
        builder.ToTable(
            "entity_type_commands",
            "general",
            t =>
            {
                t.ExcludeFromMigrations();
                t.UseSqlOutputClause(false);
            }
        );

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
        builder.Property(e => e.Key).HasColumnName("key").HasMaxLength(100);
        builder.Property(e => e.NameFa).HasColumnName("name_fa").HasMaxLength(100);
        builder.Property(e => e.NameEn).HasColumnName("name_en").HasMaxLength(100);
        builder.Property(e => e.Ordering).HasColumnName("ordering");
        builder.Property(e => e.Permissible).HasColumnName("permissible");

        builder.HasIndex(e => e.EntityTypeId);
        builder.HasIndex(e => new { e.EntityTypeId, e.Key }).IsUnique();

        builder
            .HasOne(e => e.EntityType)
            .WithMany(e => e.Commands)
            .HasForeignKey(e => e.EntityTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
