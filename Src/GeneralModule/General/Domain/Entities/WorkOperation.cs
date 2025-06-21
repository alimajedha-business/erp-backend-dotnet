using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("work_operations", Schema = "general")]
[Index("Name", Name = "UQ__work_ope__737584F65F7907DF", IsUnique = true)]
[Index("CreatorId", Name = "work_operations_creator_id_d10b2154")]
[Index("WorkDepartmentId", Name = "work_operations_work_department_id_a9f7d3e8")]
public partial class WorkOperation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("work_department_id")]
    public int? WorkDepartmentId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("WorkOperations")]
    public virtual User Creator { get; set; } = null!;

    [ForeignKey("WorkDepartmentId")]
    [InverseProperty("WorkOperations")]
    public virtual WorkDepartment? WorkDepartment { get; set; }
}
