using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("tax_tables", Schema = "payroll")]
[Index("CompanyId", Name = "tax_tables_company_id_fc5cd21d")]
[Index("CreatorId", Name = "tax_tables_creator_id_441e1f0d")]
[Index("EmploymentGroupId", Name = "tax_tables_employment_group_id_40d614e6")]
public partial class TaxTable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("employment_group_id")]
    public int EmploymentGroupId { get; set; }

    [ForeignKey("EmploymentGroupId")]
    [InverseProperty("TaxTables")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;

    [InverseProperty("TaxTable")]
    public virtual ICollection<TaxTableItem> TaxTableItems { get; set; } = new List<TaxTableItem>();
}
