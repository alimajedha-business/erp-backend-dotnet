using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("bank_templates", Schema = "accounting")]
[Index("CompanyId", Name = "bank_templates_company_id_2c793700")]
[Index("CreatorId", Name = "bank_templates_creator_id_3c99db7f")]
public partial class BankTemplate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_key")]
    [StringLength(100)]
    public string ReportKey { get; set; } = null!;

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("template")]
    [StringLength(100)]
    public string Template { get; set; } = null!;

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }
}
