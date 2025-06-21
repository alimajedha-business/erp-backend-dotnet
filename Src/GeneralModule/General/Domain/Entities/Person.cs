using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("persons", Schema = "general")]
[Index("Code", Name = "UQ__persons__357D4CF9AE8920EA", IsUnique = true)]
[Index("BirthCityId", Name = "persons_birth_city_id_9fea3d98")]
[Index("CitizenNationalityId", Name = "persons_citizen_nationality_id_ecc47f36")]
[Index("HousingStatusId", Name = "persons_housing_status_id_c8e37e67")]
[Index("MilitaryServiceStatusId", Name = "persons_military_service_status_id_31e57348")]
[Index("ReligionId", Name = "persons_religion_id_bf81e08d")]
[Index("Typ", Name = "persons_typ_611e3c8e")]
public partial class Person
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("typ")]
    [StringLength(17)]
    public string Typ { get; set; } = null!;

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public long Code { get; set; }

    [Column("economic_code")]
    [StringLength(14)]
    public string? EconomicCode { get; set; }

    [Column("economic_code_old")]
    [StringLength(14)]
    public string? EconomicCodeOld { get; set; }

    [Column("ttms_buyer_type")]
    public short? TtmsBuyerType { get; set; }

    [Column("ttms_seller_type")]
    public short? TtmsSellerType { get; set; }

    [Column("is_internal_citizenship")]
    public bool IsInternalCitizenship { get; set; }

    [Column("citizen_code")]
    [StringLength(13)]
    public string? CitizenCode { get; set; }

    [Column("natural_family")]
    [StringLength(100)]
    public string? NaturalFamily { get; set; }

    [Column("natural_father_name")]
    [StringLength(50)]
    public string? NaturalFatherName { get; set; }

    [Column("natural_national_code")]
    [StringLength(10)]
    public string? NaturalNationalCode { get; set; }

    [Column("passport_number")]
    [StringLength(10)]
    public string? PassportNumber { get; set; }

    [Column("natural_birth_date")]
    public DateOnly? NaturalBirthDate { get; set; }

    [Column("natural_sex")]
    [StringLength(1)]
    public string? NaturalSex { get; set; }

    [Column("legal_manager_name")]
    [StringLength(50)]
    public string? LegalManagerName { get; set; }

    [Column("legal_manager_family")]
    [StringLength(100)]
    public string? LegalManagerFamily { get; set; }

    [Column("legal_national_code")]
    [StringLength(11)]
    public string? LegalNationalCode { get; set; }

    [Column("legal_register_no")]
    [StringLength(50)]
    public string? LegalRegisterNo { get; set; }

    [Column("legal_establishment_date")]
    public DateOnly? LegalEstablishmentDate { get; set; }

    [Column("personnel_number")]
    [StringLength(50)]
    public string? PersonnelNumber { get; set; }

    [Column("file_number")]
    [StringLength(50)]
    public string? FileNumber { get; set; }

    [Column("id_number")]
    [StringLength(10)]
    public string? IdNumber { get; set; }

    [Column("id_issuance_place")]
    [StringLength(50)]
    public string? IdIssuancePlace { get; set; }

    [Column("id_issuance_date")]
    public DateOnly? IdIssuanceDate { get; set; }

    [Column("is_governmental")]
    public bool IsGovernmental { get; set; }

    [Column("marital_status")]
    [StringLength(6)]
    public string? MaritalStatus { get; set; }

    [Column("marriage_date")]
    public DateOnly? MarriageDate { get; set; }

    [Column("photo")]
    [StringLength(100)]
    public string? Photo { get; set; }

    [Column("service_years")]
    public short? ServiceYears { get; set; }

    [Column("service_months")]
    public short? ServiceMonths { get; set; }

    [Column("service_days")]
    public short? ServiceDays { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("birth_city_id")]
    public int? BirthCityId { get; set; }

    [Column("citizen_nationality_id")]
    public int? CitizenNationalityId { get; set; }

    [Column("housing_status_id")]
    public int? HousingStatusId { get; set; }

    [Column("military_service_status_id")]
    public int? MilitaryServiceStatusId { get; set; }

    [Column("religion_id")]
    public int? ReligionId { get; set; }

    [ForeignKey("BirthCityId")]
    [InverseProperty("People")]
    public virtual City? BirthCity { get; set; }

    [ForeignKey("CitizenNationalityId")]
    [InverseProperty("People")]
    public virtual Country? CitizenNationality { get; set; }

    [ForeignKey("HousingStatusId")]
    [InverseProperty("People")]
    public virtual HousingStatus? HousingStatus { get; set; }

    [ForeignKey("MilitaryServiceStatusId")]
    [InverseProperty("People")]
    public virtual MilitaryServiceStatus? MilitaryServiceStatus { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<PersonAddress> PersonAddresses { get; set; } = new List<PersonAddress>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonBankAccount> PersonBankAccounts { get; set; } = new List<PersonBankAccount>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonEducationalDegree> PersonEducationalDegrees { get; set; } = new List<PersonEducationalDegree>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonEmail> PersonEmails { get; set; } = new List<PersonEmail>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonFaxis> PersonFaxes { get; set; } = new List<PersonFaxis>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonMobile> PersonMobiles { get; set; } = new List<PersonMobile>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonPhone> PersonPhones { get; set; } = new List<PersonPhone>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonRelative> PersonRelatives { get; set; } = new List<PersonRelative>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonWebsite> PersonWebsites { get; set; } = new List<PersonWebsite>();

    [ForeignKey("ReligionId")]
    [InverseProperty("People")]
    public virtual Religion? Religion { get; set; }

    [InverseProperty("Person")]
    public virtual User? User { get; set; }
}
