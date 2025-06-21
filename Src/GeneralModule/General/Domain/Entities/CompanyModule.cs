using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("company_modules", Schema = "general")]
[Index("CompanyId", Name = "company_modules_company_id_98c248b4")]
[Index("ModuleId", Name = "company_modules_module_id_85bfd47f")]
public partial class CompanyModule
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("CompanyModules")]
    public virtual Company Company { get; set; } = null!;

    [ForeignKey("ModuleId")]
    [InverseProperty("CompanyModules")]
    public virtual Module Module { get; set; } = null!;
}
