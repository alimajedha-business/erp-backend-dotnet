using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class MenuItem : IViewModelTypeConfiguration<MenuItem>
{
    public Guid Id { get; set; }
    public short Order { get; set; }
    public required string NameFa { get; set; }
    public required string NameEn { get; set; }
    public string? Link { get; set; }
    public string? ShortKey { get; set; }
    public bool NewTab { get; set; }
    public string? StandardPage { get; set; }
    public string? Meta { get; set; }
    public Guid? EntityTypeId { get; set; }
    public Guid? EntityTypeCommandId { get; set; }
    public long ModuleId { get; set; }
    public Guid? ParentId { get; set; }

    public EntityType? EntityType { get; set; }
    public EntityTypeCommand? EntityTypeCommand { get; set; }
    public Module? Module { get; set; }
    public MenuItem? Parent { get; set; }
    public ICollection<MenuItem> Children { get; set; } = [];

    public void Map(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable(
            "menu_items",
            "general",
            t =>
            {
                t.ExcludeFromMigrations();
                t.UseSqlOutputClause(false);
            }
        );

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(e => e.Order).HasColumnName("order");
        builder.Property(e => e.NameFa).HasColumnName("name_fa").HasMaxLength(100);
        builder.Property(e => e.NameEn).HasColumnName("name_en").HasMaxLength(100);
        builder.Property(e => e.Link).HasColumnName("link").HasMaxLength(200);
        builder.Property(e => e.ShortKey).HasColumnName("short_key").HasMaxLength(25);
        builder.Property(e => e.NewTab).HasColumnName("new_tab");
        builder.Property(e => e.StandardPage).HasColumnName("standard_page").HasMaxLength(1000);
        builder.Property(e => e.Meta).HasColumnName("meta").HasMaxLength(200);
        builder.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
        builder.Property(e => e.EntityTypeCommandId).HasColumnName("entity_type_command_id");
        builder.Property(e => e.ModuleId).HasColumnName("module_id");
        builder.Property(e => e.ParentId).HasColumnName("parent_id");

        builder.HasIndex(e => e.ModuleId);
        builder.HasIndex(e => e.ParentId);
        
        builder.HasIndex(e => new { e.ModuleId, e.NameFa, e.ParentId }).IsUnique()
            .HasFilter("[module_id] IS NOT NULL AND [name_fa] IS NOT NULL AND [parent_id] IS NOT NULL");
        builder.HasIndex(e => new { e.ModuleId, e.NameEn, e.ParentId }).IsUnique()
            .HasFilter("[module_id] IS NOT NULL AND [name_en] IS NOT NULL AND [parent_id] IS NOT NULL");

        builder
            .HasOne(e => e.Module)
            .WithMany()
            .HasForeignKey(e => e.ModuleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.EntityType)
            .WithMany()
            .HasForeignKey(e => e.EntityTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.EntityTypeCommand)
            .WithMany()
            .HasForeignKey(e => e.EntityTypeCommandId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
