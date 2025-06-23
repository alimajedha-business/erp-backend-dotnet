using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("deduction_groups", Schema = "payroll")]
[Index("CreatorId", Name = "deduction_groups_creator_id_1ea99bf2")]
public partial class DeductionGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [InverseProperty("DeductionGroup")]
    public virtual ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();
}
