using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("company_units", Schema = "shared")]
[Index("CompanyId", Name = "company_units_company_id_0a433407")]
[Index("ParentId", Name = "company_units_parent_id_c41b621a")]
public partial class CompanyUnit
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
    public virtual ICollection<CompanyUnit> InverseParent { get; set; } = new List<CompanyUnit>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual CompanyUnit? Parent { get; set; }
}
