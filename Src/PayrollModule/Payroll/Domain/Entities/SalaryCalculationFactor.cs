using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("salary_calculation_factors", Schema = "payroll")]
[Index("CompanyId", Name = "salary_calculation_factors_company_id_b11ab28c")]
[Index("CreatorId", Name = "salary_calculation_factors_creator_id_c87701a4")]
[Index("EmploymentGroupId", Name = "salary_calculation_factors_employment_group_id_156b0b52")]
public partial class SalaryCalculationFactor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("overtime_multiplier", TypeName = "numeric(5, 2)")]
    public decimal OvertimeMultiplier { get; set; }

    [Column("night_work_multiplier", TypeName = "numeric(5, 2)")]
    public decimal NightWorkMultiplier { get; set; }

    [Column("holiday_work_multiplier", TypeName = "numeric(5, 2)")]
    public decimal HolidayWorkMultiplier { get; set; }

    [Column("shift_work1_multiplier", TypeName = "numeric(5, 2)")]
    public decimal ShiftWork1Multiplier { get; set; }

    [Column("shift_work2_multiplier", TypeName = "numeric(5, 2)")]
    public decimal ShiftWork2Multiplier { get; set; }

    [Column("shift_work3_multiplier", TypeName = "numeric(5, 2)")]
    public decimal ShiftWork3Multiplier { get; set; }

    [Column("friday_work_multiplier", TypeName = "numeric(5, 2)")]
    public decimal FridayWorkMultiplier { get; set; }

    [Column("overtime_limit_per_month")]
    public int OvertimeLimitPerMonth { get; set; }

    [Column("overtime_monthly_to_hourly")]
    public int OvertimeMonthlyToHourly { get; set; }

    [Column("holiday_work_monthly_to_hourly")]
    public int HolidayWorkMonthlyToHourly { get; set; }

    [Column("shift_work_monthly_to_hourly")]
    public int ShiftWorkMonthlyToHourly { get; set; }

    [Column("monthly_leave_allowance")]
    public int MonthlyLeaveAllowance { get; set; }

    [Column("leave_hours_per_day")]
    public int LeaveHoursPerDay { get; set; }

    [Column("absence_multiplier", TypeName = "numeric(5, 2)")]
    public decimal AbsenceMultiplier { get; set; }

    [Column("leave_calculation_basis")]
    public short LeaveCalculationBasis { get; set; }

    [Column("minimum_sick_days")]
    public short MinimumSickDays { get; set; }

    [Column("delay_monthly_to_hourly")]
    public int DelayMonthlyToHourly { get; set; }

    [Column("maximum_leave_carryover_days")]
    public int MaximumLeaveCarryoverDays { get; set; }

    [Column("enable_petty_cash_deduction")]
    public bool EnablePettyCashDeduction { get; set; }

    [Column("petty_cash_rounding_digits")]
    public int PettyCashRoundingDigits { get; set; }

    [Column("insurance_individual_share_exemption_type")]
    public short InsuranceIndividualShareExemptionType { get; set; }

    [Column("insurance_individual_share", TypeName = "numeric(5, 2)")]
    public decimal InsuranceIndividualShare { get; set; }

    [Column("insurance_employer_share", TypeName = "numeric(5, 2)")]
    public decimal InsuranceEmployerShare { get; set; }

    [Column("insurance_unemployment_share", TypeName = "numeric(5, 2)")]
    public decimal InsuranceUnemploymentShare { get; set; }

    [Column("insurance_veteran_share", TypeName = "numeric(5, 2)")]
    public decimal InsuranceVeteranShare { get; set; }

    [Column("maximum_insurable_salary", TypeName = "numeric(18, 2)")]
    public decimal? MaximumInsurableSalary { get; set; }

    [Column("minimum_insurable_salary", TypeName = "numeric(18, 2)")]
    public decimal? MinimumInsurableSalary { get; set; }

    [Column("retirement_individual_share", TypeName = "numeric(5, 2)")]
    public decimal RetirementIndividualShare { get; set; }

    [Column("retirement_employer_share", TypeName = "numeric(5, 2)")]
    public decimal RetirementEmployerShare { get; set; }

    [Column("retirement_veteran_employer_share", TypeName = "numeric(5, 2)")]
    public decimal RetirementVeteranEmployerShare { get; set; }

    [Column("retirement_veteran_share", TypeName = "numeric(5, 2)")]
    public decimal RetirementVeteranShare { get; set; }

    [Column("overtime_legal_deduction_type")]
    [StringLength(12)]
    public string OvertimeLegalDeductionType { get; set; } = null!;

    [Column("holiday_work_basis")]
    [StringLength(5)]
    public string HolidayWorkBasis { get; set; } = null!;

    [Column("holiday_work_hours_per_day")]
    public int HolidayWorkHoursPerDay { get; set; }

    [Column("holiday_work_legal_deduction_type")]
    [StringLength(12)]
    public string HolidayWorkLegalDeductionType { get; set; } = null!;

    [Column("night_work_basis")]
    [StringLength(7)]
    public string NightWorkBasis { get; set; } = null!;

    [Column("night_work_hours_per_day")]
    public int NightWorkHoursPerDay { get; set; }

    [Column("night_work_legal_deduction_type")]
    [StringLength(12)]
    public string NightWorkLegalDeductionType { get; set; } = null!;

    [Column("shift_work_basis")]
    [StringLength(7)]
    public string ShiftWorkBasis { get; set; } = null!;

    [Column("shift_work_hours_per_day")]
    public int ShiftWorkHoursPerDay { get; set; }

    [Column("shift_work_legal_deduction_type")]
    [StringLength(12)]
    public string ShiftWorkLegalDeductionType { get; set; } = null!;

    [Column("friday_work_basis")]
    [StringLength(7)]
    public string FridayWorkBasis { get; set; } = null!;

    [Column("friday_work_hours_per_day")]
    public int FridayWorkHoursPerDay { get; set; }

    [Column("friday_work_legal_deduction_type")]
    [StringLength(12)]
    public string FridayWorkLegalDeductionType { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("employment_group_id")]
    public int EmploymentGroupId { get; set; }

    [ForeignKey("EmploymentGroupId")]
    [InverseProperty("SalaryCalculationFactors")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;
}
