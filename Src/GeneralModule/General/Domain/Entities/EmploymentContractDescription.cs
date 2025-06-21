using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("employment_contract_Descriptions", Schema = "general")]
[Index("CreatorId", Name = "employment_contract_Descriptions_creator_id_2fcb5dd9")]
public partial class EmploymentContractDescription
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(500)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("EmploymentContractDescriptions")]
    public virtual User Creator { get; set; } = null!;
}
