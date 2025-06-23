using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("supplementary_insurances", Schema = "payroll")]
[Index("CompanyId", Name = "supplementary_insurances_company_id_caa970d3")]
[Index("CreatorId", Name = "supplementary_insurances_creator_id_d48733b6")]
[Index("EmploymentGroupId", Name = "supplementary_insurances_employment_group_id_2794e678")]
public partial class SupplementaryInsurance
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("supplementary_insurance_type")]
    [StringLength(12)]
    public string SupplementaryInsuranceType { get; set; } = null!;

    [Column("is_company_pay")]
    public bool IsCompanyPay { get; set; }

    [Column("apply_tax_exemption")]
    public bool? ApplyTaxExemption { get; set; }

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
    [InverseProperty("SupplementaryInsurances")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;

    [InverseProperty("SupplementaryInsurance")]
    public virtual ICollection<PersonnelSupplementaryInsuranceItem> PersonnelSupplementaryInsuranceItems { get; set; } = new List<PersonnelSupplementaryInsuranceItem>();
}
