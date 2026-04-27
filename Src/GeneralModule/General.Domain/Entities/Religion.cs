using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("religions",Schema ="General")]
public class Religion 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    //public void Map(EntityTypeBuilder<Religion> builder)
    //{
    //    builder
    //        .ToTable(nameof(Religion), "General");

    //    builder
    //        .HasIndex(e => e.Name)
    //        .IsUnique()
    //        .HasDatabaseName("UX_Religion_Name");

    //    builder
    //        .Property(e => e.Name)
    //        .HasMaxLength(50);
    //}
}
