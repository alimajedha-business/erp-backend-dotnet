using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("provinces", Schema = "general")]
[Index("CountryId", Name = "provinces_country_id_8ee0b7b3")]
public partial class Province
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [InverseProperty("Province")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [ForeignKey("CountryId")]
    [InverseProperty("Provinces")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Province")]
    public virtual ICollection<PersonAddress> PersonAddresses { get; set; } = new List<PersonAddress>();
}
