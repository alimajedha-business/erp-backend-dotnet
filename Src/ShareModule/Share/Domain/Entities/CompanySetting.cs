using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("company_settings", Schema = "shared")]
[Index("CompanyId", Name = "UQ__company___3E2672341BAB4B8B", IsUnique = true)]
public partial class CompanySetting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cost_center_level")]
    public int CostCenterLevel { get; set; }

    [Column("company_unit_level")]
    public int CompanyUnitLevel { get; set; }

    [Column("company_product_level")]
    public int CompanyProductLevel { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }
}
