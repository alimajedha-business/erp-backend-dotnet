using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("missions", Schema = "payroll")]
[Index("CompanyId", Name = "missions_company_id_3a515f6d")]
[Index("CreatorId", Name = "missions_creator_id_689b2f27")]
[Index("MissionTypeId", Name = "missions_mission_type_id_b8b52eb0")]
[Index("WorkRecordId", Name = "missions_work_record_id_71024543")]
public partial class Mission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("start_date")]
    public DateOnly? StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("count")]
    [StringLength(10)]
    public string Count { get; set; } = null!;

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("mission_type_id")]
    public int MissionTypeId { get; set; }

    [Column("work_record_id")]
    public int WorkRecordId { get; set; }

    [ForeignKey("WorkRecordId")]
    [InverseProperty("Missions")]
    public virtual WorkRecord WorkRecord { get; set; } = null!;
}
