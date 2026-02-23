using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NGErp.Base.Domain.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTime CreatedAt { get; set; }

        public string? TimeZone { get; set; }

        public Guid? CreatorId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual DateTime UpdatedAt { get; set; }

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
                .HasColumnType("bit");

            builder
                .Property<DateTime>(nameof(CreatedAt))
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
}
