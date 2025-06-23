using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_jobs", Schema = "payroll")]
[Index("AreaId", Name = "personnel_jobs_area_id_1b2fe1ab")]
[Index("CompanyId", Name = "personnel_jobs_company_id_99815a03")]
[Index("CreatorId", Name = "personnel_jobs_creator_id_1726ea18")]
[Index("EmploymentGroupId", Name = "personnel_jobs_employment_group_id_eef6c278")]
[Index("JobId", Name = "personnel_jobs_job_id_40d87bdc")]
[Index("JobPositionId", Name = "personnel_jobs_job_position_id_969b725a")]
[Index("PersonId", Name = "personnel_jobs_person_id_2bea6ee5")]
[Index("TaxWorkshopId", Name = "personnel_jobs_tax_workshop_id_82dbb82b")]
[Index("WorkDepartmentId", Name = "personnel_jobs_work_department_id_40194aa3")]
[Index("WorkOperationId", Name = "personnel_jobs_work_operation_id_371c482f")]
[Index("WorkplaceId", Name = "personnel_jobs_workplace_id_292d0d04")]
public partial class PersonnelJob
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employment_date")]
    public DateOnly? EmploymentDate { get; set; }

    [Column("exit_date")]
    public DateOnly? ExitDate { get; set; }

    [Column("settlement_date")]
    public DateOnly? SettlementDate { get; set; }

    [Column("current_contract_start_date")]
    public DateOnly? CurrentContractStartDate { get; set; }

    [Column("current_contract_end_date")]
    public DateOnly? CurrentContractEndDate { get; set; }

    [Column("employment_type")]
    [StringLength(30)]
    public string? EmploymentType { get; set; }

    [Column("insurance_type")]
    [StringLength(35)]
    public string? InsuranceType { get; set; }

    [Column("contract_type")]
    [StringLength(25)]
    public string? ContractType { get; set; }

    [Column("employee_status")]
    [StringLength(47)]
    public string? EmployeeStatus { get; set; }

    [Column("work_place_status")]
    [StringLength(40)]
    public string? WorkPlaceStatus { get; set; }

    [Column("job_category")]
    [StringLength(43)]
    public string? JobCategory { get; set; }

    [Column("is_personnel_new_in_tax_workshop")]
    public bool IsPersonnelNewInTaxWorkshop { get; set; }

    [Column("tax_deduction_type")]
    [StringLength(47)]
    public string TaxDeductionType { get; set; } = null!;

    [Column("custom_tax_deduction", TypeName = "numeric(5, 2)")]
    public decimal CustomTaxDeduction { get; set; }

    [Column("job_classification")]
    public short? JobClassification { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("area_id")]
    public int? AreaId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("employment_group_id")]
    public int EmploymentGroupId { get; set; }

    [Column("job_id")]
    public int? JobId { get; set; }

    [Column("job_position_id")]
    public int? JobPositionId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("tax_workshop_id")]
    public int? TaxWorkshopId { get; set; }

    [Column("work_department_id")]
    public int? WorkDepartmentId { get; set; }

    [Column("work_operation_id")]
    public int? WorkOperationId { get; set; }

    [Column("workplace_id")]
    public int? WorkplaceId { get; set; }

    [ForeignKey("EmploymentGroupId")]
    [InverseProperty("PersonnelJobs")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;

    [InverseProperty("PersonnelJob")]
    public virtual ICollection<PersonnelContract> PersonnelContracts { get; set; } = new List<PersonnelContract>();

    [ForeignKey("TaxWorkshopId")]
    [InverseProperty("PersonnelJobs")]
    public virtual TaxWorkshop? TaxWorkshop { get; set; }
}
