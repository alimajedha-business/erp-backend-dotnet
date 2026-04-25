using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("provinces",Schema ="General")]
public class Province 
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public string Name { get; set; } = default!;
    public int Code { get; set; }

    public Country Country { get; set; } = default!;

    //public void Map(EntityTypeBuilder<Province> builder)
    //{
    //    builder
    //        .ToTable(nameof(Province), "General");

    //    builder
    //        .HasIndex(e => e.CountryId)
    //        .HasDatabaseName("IX_Province_Country");

    //    builder
    //        .HasIndex(e => new { e.CountryId, e.Name })
    //        .IsUnique()
    //        .HasDatabaseName("UX_Province_Country_Name");

    //    builder
    //        .HasIndex(e => new { e.CountryId, e.Code })
    //        .IsUnique()
    //        .HasDatabaseName("UX_Province_Country_Code");

    //    builder
    //        .Property(e => e.Name)
    //        .HasMaxLength(50);

    //    builder
    //        .Property(e => e.Code);

    //    builder
    //        .HasOne(e => e.Country)
    //        .WithMany()
    //        .HasForeignKey(e => e.CountryId)
    //        .OnDelete(DeleteBehavior.NoAction);
    //}
}
