using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("logs", Schema = "general")]
[Index("CompanyId", Name = "logs_company_id_ed2c29a1")]
[Index("EntityTypeId", Name = "logs_entity_type_id_87a8fbba")]
[Index("ModuleId", Name = "logs_module_id_c6447665")]
[Index("UserId", Name = "logs_user_id_237f5f83")]
public partial class Log
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("object_id")]
    public int ObjectId { get; set; }

    [Column("object_repr")]
    [StringLength(200)]
    public string ObjectRepr { get; set; } = null!;

    [Column("action")]
    [StringLength(6)]
    public string Action { get; set; } = null!;

    [Column("data")]
    public string? Data { get; set; }

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
    [InverseProperty("Logs")]
    public virtual Company? Company { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("Logs")]
    public virtual EntityType EntityType { get; set; } = null!;

    [ForeignKey("ModuleId")]
    [InverseProperty("Logs")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Logs")]
    public virtual User User { get; set; } = null!;
}
