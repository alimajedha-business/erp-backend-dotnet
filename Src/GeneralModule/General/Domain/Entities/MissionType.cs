using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("mission_types", Schema = "general")]
[Index("Name", Name = "UQ__mission___72E12F1BF6C8C347", IsUnique = true)]
[Index("CreatorId", Name = "mission_types_creator_id_17915c73")]
public partial class MissionType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("has_formula")]
    public bool HasFormula { get; set; }

    [Column("formula")]
    [StringLength(300)]
    public string? Formula { get; set; }

    [Column("multiplier", TypeName = "numeric(5, 2)")]
    public decimal? Multiplier { get; set; }

    [Column("fixed_amount", TypeName = "numeric(18, 2)")]
    public decimal? FixedAmount { get; set; }

    [Column("parametric_custom_formula")]
    [StringLength(300)]
    public string? ParametricCustomFormula { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("is_hourly")]
    public bool IsHourly { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("MissionTypes")]
    public virtual User Creator { get; set; } = null!;
}
