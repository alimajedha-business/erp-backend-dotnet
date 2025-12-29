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
        public virtual DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string? TimeZone { get; set; }
        public int? UserCreationId { get; set; }
        [DefaultValue(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool IsDeleted { get; set; }
        public virtual DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int? UpdatedUserId { get; set; }

        public void MapBase(EntityTypeBuilder builder)
        {
            //builder.HasKey("Id");
            builder.Property<bool>("IsDeleted").HasColumnType("bit").HasDefaultValueSql("0");
            builder.Property<DateTime>("CreateAt").HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
            builder.Property<DateTime>("UpdatedAt").HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
            builder.Property<string?>("TimeZone").HasColumnType("nvarchar(50)");
            builder.Property<int?>("UserCreationId").HasColumnType("int");
            builder.Property<int?>("UpdatedUserId").HasColumnType("int");
            builder.HasIndex("IsDeleted");
        }

    }
}
