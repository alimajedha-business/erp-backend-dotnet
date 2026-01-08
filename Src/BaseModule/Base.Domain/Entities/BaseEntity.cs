using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NGErp.Base.Domain.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }       

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public string? TimeZone { get; set; }

        public Guid? CreatorId { get; set; }

        [DefaultValue(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool IsDeleted { get; set; }

        public virtual DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? ModifierId { get; set; }

        public void MapBase(EntityTypeBuilder builder)
        {
            builder.HasKey("Id");
            builder.Property("Id").HasDefaultValueSql("NEWID()");
            builder.Property<bool>("IsDeleted").HasColumnType("bit").HasDefaultValueSql("0");
            builder.Property<DateTime>("CreateAt").HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
            builder.Property<DateTime>("UpdatedAt").HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
            builder.Property<string?>("TimeZone").HasColumnType("nvarchar(50)");
            builder.Property<Guid?>("CreatorId").HasColumnType("uniqueidentifier");
            builder.Property<Guid?>("ModifierId").HasColumnType("uniqueidentifier");
            builder.HasIndex("IsDeleted");
        }

    }
}
