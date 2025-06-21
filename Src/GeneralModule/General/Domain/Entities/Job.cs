using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("jobs", Schema = "general")]
[Index("Name", Name = "UQ__jobs__72E12F1BF4AE1530", IsUnique = true)]
[Index("CreatorId", Name = "jobs_creator_id_56b9cd05")]
public partial class Job
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [StringLength(6)]
    public string Code { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("Jobs")]
    public virtual User Creator { get; set; } = null!;
}
