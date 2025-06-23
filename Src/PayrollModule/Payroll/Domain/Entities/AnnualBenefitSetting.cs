using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("annual_benefit_settings", Schema = "payroll")]
[Index("CompanyId", Name = "annual_benefit_settings_company_id_462fbde5")]
[Index("CreatorId", Name = "annual_benefit_settings_creator_id_ccb1f720")]
[Index("EmploymentGroupId", Name = "annual_benefit_settings_employment_group_id_9f1f84fc")]
public partial class AnnualBenefitSetting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("year")]
    public int Year { get; set; }

    [Column("minimum_daily_wage", TypeName = "numeric(18, 2)")]
    public decimal MinimumDailyWage { get; set; }

    [Column("maximum_newyear_bonus_days")]
    public int MaximumNewyearBonusDays { get; set; }

    [Column("newyear_bonus_tax_exemption", TypeName = "numeric(18, 2)")]
    public decimal NewyearBonusTaxExemption { get; set; }

    [Column("newyear_bonus_tax_exemption_is_fixed")]
    public bool NewyearBonusTaxExemptionIsFixed { get; set; }

    [Column("newyear_bonus_tax_rate", TypeName = "numeric(5, 2)")]
    public decimal NewyearBonusTaxRate { get; set; }

    [Column("newyear_bonus_calculation_is_custom")]
    public bool NewyearBonusCalculationIsCustom { get; set; }

    [Column("newyear_bonus_custom_formula")]
    [StringLength(500)]
    public string? NewyearBonusCustomFormula { get; set; }

    [Column("minimum_newyear_bonus", TypeName = "numeric(18, 2)")]
    public decimal MinimumNewyearBonus { get; set; }

    [Column("newyear_bonus_working_days_corrector")]
    public int NewyearBonusWorkingDaysCorrector { get; set; }

    [Column("newyear_bonus_monthly_to_hourly")]
    public int NewyearBonusMonthlyToHourly { get; set; }

    [Column("newyear_bonus_hours_per_day")]
    public int NewyearBonusHoursPerDay { get; set; }

    [Column("newyear_bonus_one_month_base_salary_pay")]
    public bool NewyearBonusOneMonthBaseSalaryPay { get; set; }

    [Column("newyear_bonus_fixed_amount", TypeName = "numeric(18, 2)")]
    public decimal NewyearBonusFixedAmount { get; set; }

    [Column("severance_pay_calculation_is_custom")]
    public bool SeverancePayCalculationIsCustom { get; set; }

    [Column("severance_pay_custom_formula")]
    [StringLength(500)]
    public string? SeverancePayCustomFormula { get; set; }

    [Column("leave_encashment_calculation_is_custom")]
    public bool LeaveEncashmentCalculationIsCustom { get; set; }

    [Column("leave_encashment_custom_formula")]
    [StringLength(500)]
    public string? LeaveEncashmentCustomFormula { get; set; }

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
    [InverseProperty("AnnualBenefitSettings")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;
}
