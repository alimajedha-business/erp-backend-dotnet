using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("job_positions", Schema = "general")]
[Index("Name", Name = "UQ__job_posi__72E12F1B18289596", IsUnique = true)]
[Index("CreatorId", Name = "job_positions_creator_id_361c185c")]
public partial class JobPosition
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("JobPositions")]
    public virtual User Creator { get; set; } = null!;
}
