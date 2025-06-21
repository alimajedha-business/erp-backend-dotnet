using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("cities", Schema = "general")]
[Index("Code", Name = "UQ__cities__357D4CF991D1106C", IsUnique = true)]
[Index("ProvinceId", Name = "cities_province_id_799ae9a0")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("code2")]
    public int? Code2 { get; set; }

    [Column("code3")]
    public int? Code3 { get; set; }

    [Column("province_id")]
    public int ProvinceId { get; set; }

    [InverseProperty("BirthCity")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [InverseProperty("City")]
    public virtual ICollection<PersonAddress> PersonAddresses { get; set; } = new List<PersonAddress>();

    [InverseProperty("BirthCity")]
    public virtual ICollection<PersonRelative> PersonRelatives { get; set; } = new List<PersonRelative>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Cities")]
    public virtual Province Province { get; set; } = null!;
}
