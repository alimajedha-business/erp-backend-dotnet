using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("measurement_units", Schema = "general")]
[Index("Name", Name = "UQ__measurem__72E12F1BFC5E9180", IsUnique = true)]
[Index("CreatorId", Name = "measurement_units_creator_id_adca9822")]
public partial class MeasurementUnit
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("tax_code")]
    [StringLength(10)]
    public string? TaxCode { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("MeasurementUnits")]
    public virtual User Creator { get; set; } = null!;
}
