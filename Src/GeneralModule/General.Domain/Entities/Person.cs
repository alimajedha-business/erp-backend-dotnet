using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("persons",Schema ="General")]
public class Person 
{
    public Guid Id { get; set; }
    public string Typ { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? NaturalFamily { get; set; }
    public long Code { get; set; }
    public string? EconomicCode { get; set; }
    public string? EconomicCodeOld { get; set; }
    public bool IsInternalCitizenship { get; set; } = true;
    public string? CitizenCode { get; set; }
    public string? NaturalFatherName { get; set; }
    public string? NaturalNationalCode { get; set; }
    public string? PassportNumber { get; set; }
    public Guid? BirthCityId { get; set; }
    public DateTime? NaturalBirthDate { get; set; }
    public string? NaturalSex { get; set; }
    public string? LegalManagerName { get; set; }
    public string? LegalManagerFamily { get; set; }
    public string? LegalNationalCode { get; set; }
    public string? LegalRegisterNo { get; set; }
    public DateTime? LegalEstablishmentDate { get; set; }
    public string? IdNumber { get; set; }
    public bool IsGovernmental { get; set; } = false;
    public Guid? ReligionId { get; set; }
    public string? Photo { get; set; }

    public City? BirthCity { get; set; }
    public Religion? Religion { get; set; }

    //public void Map(EntityTypeBuilder<Person> builder)
    //{
        //builder
            //.ToTable(nameof(Person), "General");

        //builder
        //    .HasIndex(e => e.Code)
        //    .IsUnique()
        //    .HasDatabaseName("UX_Person_Code");

        //builder
        //    .HasIndex(e => e.CitizenCode)
        //    .IsUnique()
        //    .HasDatabaseName("UX_Person_CitizenCode");

        //builder
        //    .HasIndex(e => e.NaturalNationalCode)
        //    .HasDatabaseName("IX_Person_NaturalNationalCode");

        //builder
        //    .HasIndex(e => e.PassportNumber)
        //    .IsUnique()
        //    .HasDatabaseName("UX_Person_PassportNumber");

        //builder
        //    .HasIndex(e => e.LegalRegisterNo)
        //    .IsUnique()
        //    .HasDatabaseName("UX_Person_LegalRegisterNo");

        //builder
        //    .HasIndex(e => e.BirthCityId)
        //    .HasDatabaseName("IX_Person_BirthCity");

        //builder
        //    .HasIndex(e => e.ReligionId)
        //    .HasDatabaseName("IX_Person_Religion");

        //builder
        //    .Property(e => e.Typ)
        //    .HasMaxLength(17);

        //builder
        //    .Property(e => e.Name)
        //    .HasMaxLength(100);

        //builder
        //    .Property(e => e.Code);

        //builder
        //    .Property(e => e.EconomicCode)
        //    .HasMaxLength(14);

        //builder
        //    .Property(e => e.EconomicCodeOld)
        //    .HasMaxLength(14);

        //builder
        //    .Property(e => e.IsInternalCitizenship)
        //    .HasDefaultValue(true);

        //builder
        //    .Property(e => e.CitizenCode)
        //    .HasMaxLength(13);

        //builder
        //    .Property(e => e.NaturalFamily)
        //    .HasMaxLength(100);

        //builder
        //    .Property(e => e.NaturalFatherName)
        //    .HasMaxLength(50);

        //builder
        //    .Property(e => e.NaturalNationalCode)
        //    .HasMaxLength(10);

        //builder
        //    .Property(e => e.PassportNumber)
        //    .HasMaxLength(10);

        //builder
        //    .Property(e => e.NaturalSex)
        //    .HasMaxLength(1);

        //builder
        //    .Property(e => e.LegalManagerName)
        //    .HasMaxLength(50);

        //builder
        //    .Property(e => e.LegalManagerFamily)
        //    .HasMaxLength(100);

        //builder
        //    .Property(e => e.LegalNationalCode)
        //    .HasMaxLength(11);

        //builder
        //    .Property(e => e.LegalRegisterNo)
        //    .HasMaxLength(50);

        //builder
        //    .Property(e => e.IdNumber)
        //    .HasMaxLength(10);

        //builder
        //    .Property(e => e.Photo)
        //    .HasMaxLength(100);

        //builder
        //    .HasOne(e => e.BirthCity)
        //    .WithMany()
        //    .HasForeignKey(e => e.BirthCityId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder
        //    .HasOne(e => e.Religion)
        //    .WithMany()
        //    .HasForeignKey(e => e.ReligionId)
        //    .OnDelete(DeleteBehavior.NoAction);
    //}
}
