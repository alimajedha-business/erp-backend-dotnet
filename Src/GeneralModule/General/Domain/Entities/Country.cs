using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("countries", Schema = "general")]
[Index("Code", Name = "UQ__countrie__357D4CF98C0F73DF", IsUnique = true)]
[Index("Name", Name = "UQ__countrie__72E12F1B7CCF347E", IsUnique = true)]
[Index("CurrencyId", Name = "countries_currency_id_3d87434c")]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("tax_code")]
    public int? TaxCode { get; set; }

    [Column("currency_id")]
    public int? CurrencyId { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("Countries")]
    public virtual Currency? Currency { get; set; }

    [InverseProperty("CitizenNationality")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [InverseProperty("Country")]
    public virtual ICollection<PersonAddress> PersonAddresses { get; set; } = new List<PersonAddress>();

    [InverseProperty("Country")]
    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
