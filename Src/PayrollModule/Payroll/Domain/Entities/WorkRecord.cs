using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("work_records", Schema = "payroll")]
[Index("CompanyId", Name = "work_records_company_id_8614ab4d")]
[Index("CreatorId", Name = "work_records_creator_id_41897573")]
[Index("PersonId", Name = "work_records_person_id_b5aa39cc")]
[Index("PersonnelContractId", Name = "work_records_personnel_contract_id_ed43badc")]
public partial class WorkRecord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("year")]
    public int Year { get; set; }

    [Column("month")]
    public int Month { get; set; }

    [Column("locked")]
    public bool Locked { get; set; }

    [Column("work_duration")]
    public int WorkDuration { get; set; }

    [Column("insurance_work_duration")]
    public int InsuranceWorkDuration { get; set; }

    [Column("overtime")]
    public int Overtime { get; set; }

    [Column("night_work")]
    public int NightWork { get; set; }

    [Column("holiday_work")]
    public int HolidayWork { get; set; }

    [Column("shift_work1")]
    public int ShiftWork1 { get; set; }

    [Column("shift_work2")]
    public int ShiftWork2 { get; set; }

    [Column("shift_work3")]
    public int ShiftWork3 { get; set; }

    [Column("friday_work")]
    public int FridayWork { get; set; }

    [Column("hourly_leave")]
    public int HourlyLeave { get; set; }

    [Column("daily_leave")]
    public short DailyLeave { get; set; }

    [Column("absence")]
    public int Absence { get; set; }

    [Column("sick_day")]
    public short SickDay { get; set; }

    [Column("mission")]
    public short Mission { get; set; }

    [Column("leave_without_pay")]
    public short LeaveWithoutPay { get; set; }

    [Column("minimum_sick_day")]
    public short MinimumSickDay { get; set; }

    [Column("overtime_amount", TypeName = "numeric(18, 2)")]
    public decimal OvertimeAmount { get; set; }

    [Column("night_work_amount", TypeName = "numeric(18, 2)")]
    public decimal NightWorkAmount { get; set; }

    [Column("holiday_work_amount", TypeName = "numeric(18, 2)")]
    public decimal HolidayWorkAmount { get; set; }

    [Column("shift_work1_amount", TypeName = "numeric(18, 2)")]
    public decimal ShiftWork1Amount { get; set; }

    [Column("shift_work2_amount", TypeName = "numeric(18, 2)")]
    public decimal ShiftWork2Amount { get; set; }

    [Column("shift_work3_amount", TypeName = "numeric(18, 2)")]
    public decimal ShiftWork3Amount { get; set; }

    [Column("friday_work_amount", TypeName = "numeric(18, 2)")]
    public decimal FridayWorkAmount { get; set; }

    [Column("continuous_cash_wage_amount", TypeName = "numeric(18, 2)")]
    public decimal ContinuousCashWageAmount { get; set; }

    [Column("continuous_non_cash_benefit_amount", TypeName = "numeric(18, 2)")]
    public decimal ContinuousNonCashBenefitAmount { get; set; }

    [Column("noncontinuous_non_cash_benefit_amount", TypeName = "numeric(18, 2)")]
    public decimal NoncontinuousNonCashBenefitAmount { get; set; }

    [Column("noncontinuous_cash_pay", TypeName = "numeric(18, 2)")]
    public decimal NoncontinuousCashPay { get; set; }

    [Column("tax", TypeName = "numeric(18, 2)")]
    public decimal Tax { get; set; }

    [Column("insurable")]
    [StringLength(10)]
    public string Insurable { get; set; } = null!;

    [Column("insurance_individual_share", TypeName = "numeric(18, 2)")]
    public decimal InsuranceIndividualShare { get; set; }

    [Column("insurance_employer_share", TypeName = "numeric(18, 2)")]
    public decimal InsuranceEmployerShare { get; set; }

    [Column("insurance_unemployment_share", TypeName = "numeric(18, 2)")]
    public decimal InsuranceUnemploymentShare { get; set; }

    [Column("insurance_veteran_share", TypeName = "numeric(18, 2)")]
    public decimal InsuranceVeteranShare { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("personnel_contract_id")]
    public int PersonnelContractId { get; set; }

    [InverseProperty("WorkRecord")]
    public virtual ICollection<BenefitCalculation> BenefitCalculations { get; set; } = new List<BenefitCalculation>();

    [InverseProperty("WorkRecord")]
    public virtual ICollection<DeductionCalculation> DeductionCalculations { get; set; } = new List<DeductionCalculation>();

    [InverseProperty("WorkRecord")]
    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    [ForeignKey("PersonnelContractId")]
    [InverseProperty("WorkRecords")]
    public virtual PersonnelContract PersonnelContract { get; set; } = null!;
}
