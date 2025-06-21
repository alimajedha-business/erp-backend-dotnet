using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("work_departments", Schema = "general")]
[Index("Name", Name = "UQ__work_dep__72E12F1B9A5F7AEA", IsUnique = true)]
[Index("CreatorId", Name = "work_departments_creator_id_7fdcedc2")]
[Index("WorkPlaceId", Name = "work_departments_work_place_id_182cba09")]
public partial class WorkDepartment
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

    [Column("work_place_id")]
    public int WorkPlaceId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("WorkDepartments")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("WorkDepartment")]
    public virtual ICollection<WorkOperation> WorkOperations { get; set; } = new List<WorkOperation>();

    [ForeignKey("WorkPlaceId")]
    [InverseProperty("WorkDepartments")]
    public virtual Workplace WorkPlace { get; set; } = null!;
}
