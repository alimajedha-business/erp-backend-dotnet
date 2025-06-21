using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("employment_contract_titles", Schema = "general")]
[Index("Name", Name = "UQ__employme__72E12F1B6B3538D6", IsUnique = true)]
[Index("CreatorId", Name = "employment_contract_titles_creator_id_f0cd95e9")]
public partial class EmploymentContractTitle
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("EmploymentContractTitles")]
    public virtual User Creator { get; set; } = null!;
}
