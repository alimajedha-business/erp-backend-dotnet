using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("salary_increase_formulas", Schema = "payroll")]
[Index("CreatorId", Name = "salary_increase_formulas_creator_id_e66fe259")]
[Index("EmploymentContractTemplateItemId", Name = "salary_increase_formulas_employment_contract_template_item_id_3d8ec62b")]
[Index("SalaryIncreaseId", Name = "salary_increase_formulas_salary_increase_id_10108bf7")]
public partial class SalaryIncreaseFormula
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("formula_or_amount")]
    [StringLength(500)]
    public string FormulaOrAmount { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("employment_contract_template_item_id")]
    public int EmploymentContractTemplateItemId { get; set; }

    [Column("salary_increase_id")]
    public int SalaryIncreaseId { get; set; }

    [ForeignKey("EmploymentContractTemplateItemId")]
    [InverseProperty("SalaryIncreaseFormulas")]
    public virtual EmploymentContractTemplateItem EmploymentContractTemplateItem { get; set; } = null!;

    [ForeignKey("SalaryIncreaseId")]
    [InverseProperty("SalaryIncreaseFormulas")]
    public virtual SalaryIncrease SalaryIncrease { get; set; } = null!;
}
