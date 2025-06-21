using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("job_ranks", Schema = "general")]
[Index("Name", Name = "UQ__job_rank__72E12F1B78A994B0", IsUnique = true)]
[Index("CreatorId", Name = "job_ranks_creator_id_06b134a2")]
public partial class JobRank
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("JobRanks")]
    public virtual User Creator { get; set; } = null!;
}
