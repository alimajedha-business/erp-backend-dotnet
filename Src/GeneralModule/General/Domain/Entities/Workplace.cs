using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("workplaces", Schema = "general")]
[Index("Name", Name = "UQ__workplac__72E12F1B6F317AC7", IsUnique = true)]
[Index("CreatorId", Name = "workplaces_creator_id_0cc80a6d")]
public partial class Workplace
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("Workplaces")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("WorkPlace")]
    public virtual ICollection<WorkDepartment> WorkDepartments { get; set; } = new List<WorkDepartment>();
}
