using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_addresses", Schema = "general")]
[Index("CityId", Name = "person_addresses_city_id_bda2a750")]
[Index("CountryId", Name = "person_addresses_country_id_1fb41b6e")]
[Index("PersonId", Name = "person_addresses_person_id_d67d7fc6")]
[Index("ProvinceId", Name = "person_addresses_province_id_6ed2a37d")]
public partial class PersonAddress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("address")]
    [StringLength(300)]
    public string Address { get; set; } = null!;

    [Column("postal_code")]
    [StringLength(10)]
    public string? PostalCode { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("province_id")]
    public int ProvinceId { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("PersonAddresses")]
    public virtual City City { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("PersonAddresses")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonAddresses")]
    public virtual Person Person { get; set; } = null!;

    [ForeignKey("ProvinceId")]
    [InverseProperty("PersonAddresses")]
    public virtual Province Province { get; set; } = null!;
}
