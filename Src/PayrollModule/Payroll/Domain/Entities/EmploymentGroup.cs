using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("employment_groups", Schema = "payroll")]
[Index("CompanyId", Name = "employment_groups_company_id_736382ff")]
[Index("CreatorId", Name = "employment_groups_creator_id_ba817fe7")]
public partial class EmploymentGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("is_hourly_wage")]
    public bool IsHourlyWage { get; set; }

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

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<AnnualBenefitSetting> AnnualBenefitSettings { get; set; } = new List<AnnualBenefitSetting>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<EmploymentContractTemplate> EmploymentContractTemplates { get; set; } = new List<EmploymentContractTemplate>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<PersonnelJob> PersonnelJobs { get; set; } = new List<PersonnelJob>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<SalaryCalculationFactor> SalaryCalculationFactors { get; set; } = new List<SalaryCalculationFactor>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<SalaryIncrease> SalaryIncreases { get; set; } = new List<SalaryIncrease>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<SupplementaryInsurance> SupplementaryInsurances { get; set; } = new List<SupplementaryInsurance>();

    [InverseProperty("EmploymentGroup")]
    public virtual ICollection<TaxTable> TaxTables { get; set; } = new List<TaxTable>();
}
