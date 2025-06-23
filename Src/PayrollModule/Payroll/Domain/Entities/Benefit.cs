using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("benefits", Schema = "payroll")]
[Index("BenefitGroupId", Name = "benefits_benefit_group_id_2bee0bf8")]
[Index("CompanyId", Name = "benefits_company_id_45b4af61")]
[Index("CreatorId", Name = "benefits_creator_id_a87a7211")]
public partial class Benefit
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("in_tax_calculation")]
    public bool InTaxCalculation { get; set; }

    [Column("benefit_type")]
    [StringLength(30)]
    public string BenefitType { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("in_insurance_calculation")]
    public bool InInsuranceCalculation { get; set; }

    [Column("based_on_work_days")]
    public bool BasedOnWorkDays { get; set; }

    [Column("is_continuous")]
    public bool IsContinuous { get; set; }

    [Column("is_cash")]
    public bool IsCash { get; set; }

    [Column("formula_or_amount")]
    [StringLength(500)]
    public string? FormulaOrAmount { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("benefit_group_id")]
    public int BenefitGroupId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [InverseProperty("Benefit")]
    public virtual ICollection<BenefitCalculation> BenefitCalculations { get; set; } = new List<BenefitCalculation>();

    [ForeignKey("BenefitGroupId")]
    [InverseProperty("Benefits")]
    public virtual BenefitGroup BenefitGroup { get; set; } = null!;
}
