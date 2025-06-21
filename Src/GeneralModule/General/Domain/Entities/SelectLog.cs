using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("select_logs", Schema = "general")]
[Index("CompanyId", Name = "select_logs_company_id_36cbe9f3")]
[Index("EntityTypeId", Name = "select_logs_entity_type_id_1ff059a2")]
[Index("ModuleId", Name = "select_logs_module_id_94ef8091")]
[Index("UserId", Name = "select_logs_user_id_e7ccfdfc")]
public partial class SelectLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("filters_used")]
    public string? FiltersUsed { get; set; }

    [Column("command")]
    [StringLength(30)]
    public string? Command { get; set; }

    [Column("ip")]
    [StringLength(39)]
    public string Ip { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("SelectLogs")]
    public virtual Company? Company { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("SelectLogs")]
    public virtual EntityType EntityType { get; set; } = null!;

    [ForeignKey("ModuleId")]
    [InverseProperty("SelectLogs")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("SelectLogs")]
    public virtual User User { get; set; } = null!;
}
