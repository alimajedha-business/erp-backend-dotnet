using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("benefit_groups", Schema = "payroll")]
[Index("CreatorId", Name = "benefit_groups_creator_id_356bfaa9")]
public partial class BenefitGroup
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

    [InverseProperty("BenefitGroup")]
    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
}
