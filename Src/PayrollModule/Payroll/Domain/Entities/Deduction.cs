using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("deductions", Schema = "payroll")]
[Index("CompanyId", Name = "deductions_company_id_0bce9c53")]
[Index("CreatorId", Name = "deductions_creator_id_4de17518")]
[Index("DeductionGroupId", Name = "deductions_deduction_group_id_87a9be7d")]
public partial class Deduction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("based_on_work_days")]
    public bool BasedOnWorkDays { get; set; }

    [Column("is_continuous")]
    public bool IsContinuous { get; set; }

    [Column("deduction_type")]
    [StringLength(35)]
    public string DeductionType { get; set; } = null!;

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

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("deduction_group_id")]
    public int DeductionGroupId { get; set; }

    [InverseProperty("Deduction")]
    public virtual ICollection<DeductionCalculation> DeductionCalculations { get; set; } = new List<DeductionCalculation>();

    [ForeignKey("DeductionGroupId")]
    [InverseProperty("Deductions")]
    public virtual DeductionGroup DeductionGroup { get; set; } = null!;
}
