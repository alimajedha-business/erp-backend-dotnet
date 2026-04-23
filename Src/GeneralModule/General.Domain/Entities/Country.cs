using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("countries",Schema ="General")]
public class Country 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Code { get; set; }
    public Guid? CurrencyId { get; set; }
    public int? TaxCode { get; set; }
    public string? Iso { get; set; }

    public Currency? Currency { get; set; }

    //public void Map(EntityTypeBuilder<Country> builder)
    //{
    //    builder
    //        .ToTable(nameof(Country), "General");

        //    builder
        //        .HasIndex(e => e.Name)
        //        .IsUnique()
        //        .HasDatabaseName("UX_Country_Name");

        //    builder
        //        .HasIndex(e => e.Code)
        //        .IsUnique()
        //        .HasDatabaseName("UX_Country_Code");

        //    builder
        //        .HasIndex(e => e.CurrencyId)
        //        .HasDatabaseName("IX_Country_Currency");

        //    builder
        //        .Property(e => e.Name)
        //        .HasMaxLength(50);

        //    builder
        //        .Property(e => e.Code);

        //    builder
        //        .Property(e => e.TaxCode);

        //    builder
        //        .Property(e => e.Iso)
        //        .HasMaxLength(3);

        //    builder
        //        .HasOne(e => e.Currency)
        //        .WithMany(e => e.Countries)
        //        .HasForeignKey(e => e.CurrencyId)
        //        .OnDelete(DeleteBehavior.NoAction);
    //}
}
