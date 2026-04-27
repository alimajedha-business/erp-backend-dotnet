using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("cities",Schema ="General")]
public class City 
{
    public Guid Id { get; set; }
    public Guid ProvinceId { get; set; }
    public string Name { get; set; } = default!;
    public int Code { get; set; }
    public int? Code2 { get; set; }
    public int? Code3 { get; set; }

    public Province Province { get; set; } = default!;

    //public void Map(EntityTypeBuilder<City> builder)
    //{
    //    builder
    //        .ToTable(nameof(City), "General");

    //    builder
    //        .HasIndex(e => e.ProvinceId)
    //        .HasDatabaseName("IX_City_Province");

    //    builder
    //        .HasIndex(e => e.Code)
    //        .IsUnique()
    //        .HasDatabaseName("UX_City_Code");

    //    builder
    //        .HasIndex(e => e.Code2)
    //        .IsUnique()
    //        .HasDatabaseName("UX_City_Code2");

    //    builder
    //        .HasIndex(e => e.Code3)
    //        .IsUnique()
    //        .HasDatabaseName("UX_City_Code3");

    //    builder
    //        .Property(e => e.Name)
    //        .HasMaxLength(50);

    //    builder
    //        .Property(e => e.Code);

    //    builder
    //        .Property(e => e.Code2);

    //    builder
    //        .Property(e => e.Code3);

    //    builder
    //        .HasOne(e => e.Province)
    //        .WithMany()
    //        .HasForeignKey(e => e.ProvinceId)
    //        .OnDelete(DeleteBehavior.NoAction);
    //}
}
