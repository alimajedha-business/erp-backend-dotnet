using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("report_templates", Schema = "shared")]
[Index("CompanyId", Name = "report_templates_company_id_d546fa98")]
[Index("CreatorId", Name = "report_templates_creator_id_1650956e")]
[Index("ModuleId", Name = "report_templates_module_id_e5a4ce8d")]
public partial class ReportTemplate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_key")]
    [StringLength(100)]
    public string ReportKey { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("template")]
    [StringLength(100)]
    public string Template { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }
}
