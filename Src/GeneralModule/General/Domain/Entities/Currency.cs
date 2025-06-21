using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("currencies", Schema = "general")]
[Index("Code", Name = "UQ__currenci__357D4CF907683F55", IsUnique = true)]
public partial class Currency
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("symbol")]
    [StringLength(10)]
    public string? Symbol { get; set; }

    [Column("iso")]
    [StringLength(3)]
    public string Iso { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
