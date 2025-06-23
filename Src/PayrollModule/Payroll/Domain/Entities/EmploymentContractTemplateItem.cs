using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("employment_contract_template_items", Schema = "payroll")]
[Index("CreatorId", Name = "employment_contract_template_items_creator_id_fd47efb8")]
[Index("EctItemGroupId", Name = "employment_contract_template_items_ect_item_group_id_7f2f5383")]
[Index("EmploymentContractTemplateId", Name = "employment_contract_template_items_employment_contract_template_id_c433e8e3")]
public partial class EmploymentContractTemplateItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("row")]
    public int Row { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("calculation_type")]
    [StringLength(25)]
    public string CalculationType { get; set; } = null!;

    [Column("is_monthly")]
    public bool IsMonthly { get; set; }

    [Column("in_tax_calculation")]
    public bool InTaxCalculation { get; set; }

    [Column("in_insurance_calculation")]
    public bool InInsuranceCalculation { get; set; }

    [Column("in_saving_calculation")]
    public bool InSavingCalculation { get; set; }

    [Column("in_retirement_calculation")]
    public bool InRetirementCalculation { get; set; }

    [Column("is_base_salary")]
    public bool IsBaseSalary { get; set; }

    [Column("in_overtime_calculation")]
    public bool InOvertimeCalculation { get; set; }

    [Column("in_night_shift_calculation")]
    public bool InNightShiftCalculation { get; set; }

    [Column("in_shift_calculation")]
    public bool InShiftCalculation { get; set; }

    [Column("in_holiday_work_calculation")]
    public bool InHolidayWorkCalculation { get; set; }

    [Column("in_friday_work_calculation")]
    public bool InFridayWorkCalculation { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("ect_item_group_id")]
    public int EctItemGroupId { get; set; }

    [Column("employment_contract_template_id")]
    public int EmploymentContractTemplateId { get; set; }

    [ForeignKey("EctItemGroupId")]
    [InverseProperty("EmploymentContractTemplateItems")]
    public virtual EmploymentContractTemplateItemGroup EctItemGroup { get; set; } = null!;

    [ForeignKey("EmploymentContractTemplateId")]
    [InverseProperty("EmploymentContractTemplateItems")]
    public virtual EmploymentContractTemplate EmploymentContractTemplate { get; set; } = null!;

    [InverseProperty("EmploymentContractTemplateItem")]
    public virtual ICollection<PersonnelContractItem> PersonnelContractItems { get; set; } = new List<PersonnelContractItem>();

    [InverseProperty("EmploymentContractTemplateItem")]
    public virtual ICollection<SalaryIncreaseFormula> SalaryIncreaseFormulas { get; set; } = new List<SalaryIncreaseFormula>();
}
