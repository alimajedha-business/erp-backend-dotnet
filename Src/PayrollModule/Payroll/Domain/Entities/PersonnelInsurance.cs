using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_insurances", Schema = "payroll")]
[Index("CompanyId", Name = "personnel_insurances_company_id_9eee97ae")]
[Index("CreatorId", Name = "personnel_insurances_creator_id_cab02e90")]
[Index("InsuranceWorkshopId", Name = "personnel_insurances_insurance_workshop_id_944b8b6b")]
[Index("PersonId", Name = "personnel_insurances_person_id_f263812d")]
public partial class PersonnelInsurance
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("insurance_number")]
    [StringLength(10)]
    public string? InsuranceNumber { get; set; }

    [Column("apply_insurance_deduction")]
    public bool ApplyInsuranceDeduction { get; set; }

    [Column("previous_insurable_amount", TypeName = "numeric(18, 2)")]
    public decimal? PreviousInsurableAmount { get; set; }

    [Column("individual_share_calculation")]
    [StringLength(30)]
    public string? IndividualShareCalculation { get; set; }

    [Column("individual_share_calculation_input")]
    [StringLength(16)]
    public string? IndividualShareCalculationInput { get; set; }

    [Column("employer_share_calculation")]
    [StringLength(30)]
    public string? EmployerShareCalculation { get; set; }

    [Column("employer_share_calculation_input")]
    [StringLength(16)]
    public string? EmployerShareCalculationInput { get; set; }

    [Column("unemployment_share_calculation")]
    [StringLength(30)]
    public string? UnemploymentShareCalculation { get; set; }

    [Column("unemployment_share_calculation_input")]
    [StringLength(16)]
    public string? UnemploymentShareCalculationInput { get; set; }

    [Column("fixed_insurable_amount", TypeName = "numeric(18, 2)")]
    public decimal? FixedInsurableAmount { get; set; }

    [Column("hard_work_multiplier", TypeName = "numeric(5, 2)")]
    public decimal? HardWorkMultiplier { get; set; }

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

    [Column("insurance_workshop_id")]
    public int? InsuranceWorkshopId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("InsuranceWorkshopId")]
    [InverseProperty("PersonnelInsurances")]
    public virtual InsuranceWorkshop? InsuranceWorkshop { get; set; }

    [InverseProperty("PersonnelInsurance")]
    public virtual ICollection<PersonnelContract> PersonnelContracts { get; set; } = new List<PersonnelContract>();
}
