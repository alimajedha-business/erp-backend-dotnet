using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class EntityType : IViewModelTypeConfiguration<EntityType>
{
    public Guid Id { get; set; }
    public int? ContentTypeId { get; set; }
    public long ModuleId { get; set; }
    public required string Key { get; set; }
    public required string NameFa { get; set; }
    public required string NameEn { get; set; }
    public required string Code { get; set; }
    public bool Readable { get; set; }
    public bool Creatable { get; set; }
    public bool Editable { get; set; }
    public bool Deletable { get; set; }
    public bool Loggable { get; set; }
    public bool Printable { get; set; }
    public bool Importable { get; set; }
    public bool Exportable { get; set; }
    public bool IfNotCreator { get; set; }
    public bool HasRestriction { get; set; }
    public bool Permissible { get; set; }
    public bool HasConstraint { get; set; }
    public short? Ordering { get; set; }
    public long? InherentlyModuleId { get; set; }

    public Module? Module { get; set; }
    public ICollection<EntityTypeCommand> Commands { get; set; } = [];

    public void Map(EntityTypeBuilder<EntityType> builder)
    {
        builder.ToTable(
            "entity_types",
            "general",
            t =>
            {
                t.UseSqlOutputClause(false);
            }
        );

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
        builder.Property(e => e.ModuleId).HasColumnName("module_id");
        builder.Property(e => e.Key).HasColumnName("key").HasMaxLength(100);
        builder.Property(e => e.NameFa).HasColumnName("name_fa").HasMaxLength(100);
        builder.Property(e => e.NameEn).HasColumnName("name_en").HasMaxLength(100);
        builder.Property(e => e.Code).HasColumnName("code").HasMaxLength(4);
        builder.Property(e => e.Readable).HasColumnName("readable");
        builder.Property(e => e.Creatable).HasColumnName("creatable");
        builder.Property(e => e.Editable).HasColumnName("editable");
        builder.Property(e => e.Deletable).HasColumnName("deletable");
        builder.Property(e => e.Loggable).HasColumnName("loggable");
        builder.Property(e => e.Printable).HasColumnName("printable");
        builder.Property(e => e.Importable).HasColumnName("importable");
        builder.Property(e => e.Exportable).HasColumnName("exportable");
        builder.Property(e => e.IfNotCreator).HasColumnName("if_not_creator");
        builder.Property(e => e.HasRestriction).HasColumnName("has_restriction");
        builder.Property(e => e.Permissible).HasColumnName("permissible");
        builder.Property(e => e.HasConstraint).HasColumnName("has_constraint");
        builder.Property(e => e.Ordering).HasColumnName("ordering");
        builder.Property(e => e.InherentlyModuleId).HasColumnName("inherently_module_id");

        builder.HasIndex(e => e.ModuleId);
        builder.HasIndex(e => new { e.ModuleId, e.Key }).IsUnique();
        builder.HasIndex(e => new { e.ModuleId, e.NameFa }).IsUnique();
        builder.HasIndex(e => new { e.ModuleId, e.NameEn }).IsUnique();

        builder
            .HasOne(e => e.Module)
            .WithMany(e => e.EntityTypes)
            .HasForeignKey(e => e.ModuleId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
