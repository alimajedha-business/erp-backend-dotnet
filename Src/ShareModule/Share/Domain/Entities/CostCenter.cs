using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("cost_centers", Schema = "shared")]
[Index("CompanyId", Name = "cost_centers_company_id_5390b4ec")]
[Index("ParentId", Name = "cost_centers_parent_id_eba90f6c")]
public partial class CostCenter
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("level")]
    public short Level { get; set; }

    [Column("last_level")]
    public bool LastLevel { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    [StringLength(18)]
    public string Code { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<CostCenter> InverseParent { get; set; } = new List<CostCenter>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual CostCenter? Parent { get; set; }
}
