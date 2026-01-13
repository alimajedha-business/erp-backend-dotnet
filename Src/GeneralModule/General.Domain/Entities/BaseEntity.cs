using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NGErp.General.Domain.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual DateTime CreateAt { get; set; } = DateTime.UtcNow;

    public string TimeZone { get; set; } = default!;

    public Guid? CreatorId { get; set; }

    [DefaultValue(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public bool IsDeleted { get; set; }

    public virtual DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid? ModifierId { get; set; }

    public static void MapBase(EntityTypeBuilder builder)
    {
        builder
            .HasKey("Id");

        builder
            .Property(nameof(Id))
            .HasDefaultValueSql("NEWID()");

        builder
            .Property<bool>(nameof(IsDeleted))
            .HasColumnType("bit")
            .HasDefaultValueSql("0");

        builder
            .Property<DateTime>(nameof(CreateAt))
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property<DateTime>(nameof(UpdatedAt))
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property<string?>(nameof(TimeZone))
            .HasColumnType("nvarchar(50)");

        builder
            .Property<Guid?>(nameof(CreatorId))
            .HasColumnType("uniqueidentifier");

        builder
            .Property<Guid?>(nameof(ModifierId))
            .HasColumnType("uniqueidentifier");

        builder
            .HasIndex("IsDeleted");
    }

}