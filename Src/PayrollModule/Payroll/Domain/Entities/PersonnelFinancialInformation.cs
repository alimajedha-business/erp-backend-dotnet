using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_financial_information", Schema = "payroll")]
[Index("CompanyId", Name = "personnel_financial_information_company_id_e59ef3ed")]
[Index("CompanyUnitId", Name = "personnel_financial_information_company_unit_id_a87e9782")]
[Index("CostCenterId", Name = "personnel_financial_information_cost_center_id_81e53bf4")]
[Index("CreatorId", Name = "personnel_financial_information_creator_id_70a8f64a")]
[Index("PersonBankAccountId", Name = "personnel_financial_information_person_bank_account_id_5285782a")]
[Index("PersonId", Name = "personnel_financial_information_person_id_31e7105d")]
[Index("ProjectId", Name = "personnel_financial_information_project_id_896e73c9")]
public partial class PersonnelFinancialInformation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("special_tax_exemption", TypeName = "numeric(18, 2)")]
    public decimal? SpecialTaxExemption { get; set; }

    [Column("savings_1_amount", TypeName = "numeric(18, 2)")]
    public decimal? Savings1Amount { get; set; }

    [Column("savings_1_deduction")]
    [StringLength(37)]
    public string? Savings1Deduction { get; set; }

    [Column("savings_2_amount", TypeName = "numeric(18, 2)")]
    public decimal? Savings2Amount { get; set; }

    [Column("savings_2_deduction")]
    [StringLength(37)]
    public string? Savings2Deduction { get; set; }

    [Column("savings_2_legal_deduction_type")]
    [StringLength(12)]
    public string? Savings2LegalDeductionType { get; set; }

    [Column("savings_3_amount", TypeName = "numeric(18, 2)")]
    public decimal? Savings3Amount { get; set; }

    [Column("savings_3_deduction")]
    [StringLength(37)]
    public string? Savings3Deduction { get; set; }

    [Column("savings_3_legal_deduction_type1")]
    [StringLength(12)]
    public string? Savings3LegalDeductionType1 { get; set; }

    [Column("savings_4_amount", TypeName = "numeric(18, 2)")]
    public decimal? Savings4Amount { get; set; }

    [Column("savings_4_deduction")]
    [StringLength(37)]
    public string? Savings4Deduction { get; set; }

    [Column("savings_4_legal_deduction_type1")]
    [StringLength(12)]
    public string? Savings4LegalDeductionType1 { get; set; }

    [Column("overtime_hourly_wage", TypeName = "numeric(18, 2)")]
    public decimal? OvertimeHourlyWage { get; set; }

    [Column("holiday_work_hourly_wage", TypeName = "numeric(18, 2)")]
    public decimal? HolidayWorkHourlyWage { get; set; }

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

    [Column("company_unit_id")]
    public int? CompanyUnitId { get; set; }

    [Column("cost_center_id")]
    public int? CostCenterId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("person_bank_account_id")]
    public int? PersonBankAccountId { get; set; }

    [Column("project_id")]
    public int? ProjectId { get; set; }

    [InverseProperty("PersonnelFinancialInformation")]
    public virtual ICollection<PersonnelContract> PersonnelContracts { get; set; } = new List<PersonnelContract>();
}
