using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

[Table("persons",Schema ="General")]
public class Person 
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("typ")]
    public string Typ { get; set; } = default!;

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("natural_family")]
    public string? NaturalFamily { get; set; }

    [Column("code")]
    public long Code { get; set; }

    [Column("economic_code")]
    public string? EconomicCode { get; set; }

    [Column("economic_code_old")]
    public string? EconomicCodeOld { get; set; }

    [Column("is_internal_citizenship")]
    public bool IsInternalCitizenship { get; set; } = true;

    [Column("citizen_code")]
    public string? CitizenCode { get; set; }

    [Column("natural_father_name")]
    public string? NaturalFatherName { get; set; }

    [Column("natural_national_code")]
    public string? NaturalNationalCode { get; set; }

    [Column("passport_number")]
    public string? PassportNumber { get; set; }

    [Column("birth_city_id")]
    public Guid? BirthCityId { get; set; }

    [Column("natural_birth_date")]
    public DateTime? NaturalBirthDate { get; set; }

    [Column("natural_sex")]
    public string? NaturalSex { get; set; }

    [Column("legal_manager_name")]
    public string? LegalManagerName { get; set; }

    [Column("legal_manager_family")]
    public string? LegalManagerFamily { get; set; }

    [Column("legal_national_code")]
    public string? LegalNationalCode { get; set; }

    [Column("legal_register_no")]
    public string? LegalRegisterNo { get; set; }

    [Column("legal_establishment_date")]
    public DateTime? LegalEstablishmentDate { get; set; }

    [Column("id_number")]
    public string? IdNumber { get; set; }

    [Column("is_governmental")]
    public bool IsGovernmental { get; set; } = false;

    [Column("religion_id")]
    public Guid? ReligionId { get; set; }

    [Column("photo")]
    public string? Photo { get; set; }
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
