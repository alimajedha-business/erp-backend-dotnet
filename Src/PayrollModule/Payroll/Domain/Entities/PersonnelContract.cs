using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_contracts", Schema = "payroll")]
[Index("CompanyId", Name = "personnel_contracts_company_id_e3b5af63")]
[Index("CreatorId", Name = "personnel_contracts_creator_id_5da4b330")]
[Index("EmploymentContractTemplateId", Name = "personnel_contracts_employment_contract_template_id_57942850")]
[Index("EmploymentContractTitleId", Name = "personnel_contracts_employment_contract_title_id_d26bc982")]
[Index("JobRankId", Name = "personnel_contracts_job_rank_id_6d3ed5c9")]
[Index("PersonId", Name = "personnel_contracts_person_id_19f0e577")]
[Index("PersonnelFinancialInformationId", Name = "personnel_contracts_personnel_financial_information_id_413e25a0")]
[Index("PersonnelInsuranceId", Name = "personnel_contracts_personnel_insurance_id_f9e2914b")]
[Index("PersonnelJobId", Name = "personnel_contracts_personnel_job_id_2561e515")]
public partial class PersonnelContract
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("state")]
    [StringLength(17)]
    public string State { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("effective_date")]
    public DateOnly EffectiveDate { get; set; }

    [Column("issuance_date")]
    public DateOnly IssuanceDate { get; set; }

    [Column("secretariat_number")]
    [StringLength(20)]
    public string? SecretariatNumber { get; set; }

    [Column("description")]
    [StringLength(3000)]
    public string? Description { get; set; }

    [Column("job_classification")]
    public short? JobClassification { get; set; }

    [Column("custom_personnel_job")]
    public bool CustomPersonnelJob { get; set; }

    [Column("custom_personnel_insurance")]
    public bool CustomPersonnelInsurance { get; set; }

    [Column("custom_personnel_financial_information")]
    public bool CustomPersonnelFinancialInformation { get; set; }

    [Column("custom_employment_contract_template")]
    public bool CustomEmploymentContractTemplate { get; set; }

    [Column("base_salary", TypeName = "numeric(18, 2)")]
    public decimal BaseSalary { get; set; }

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

    [Column("employment_contract_template_id")]
    public int EmploymentContractTemplateId { get; set; }

    [Column("employment_contract_title_id")]
    public int? EmploymentContractTitleId { get; set; }

    [Column("job_rank_id")]
    public int? JobRankId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("personnel_financial_information_id")]
    public int PersonnelFinancialInformationId { get; set; }

    [Column("personnel_insurance_id")]
    public int PersonnelInsuranceId { get; set; }

    [Column("personnel_job_id")]
    public int PersonnelJobId { get; set; }

    [ForeignKey("EmploymentContractTemplateId")]
    [InverseProperty("PersonnelContracts")]
    public virtual EmploymentContractTemplate EmploymentContractTemplate { get; set; } = null!;

    [InverseProperty("PersonnelContract")]
    public virtual ICollection<PersonnelContractItem> PersonnelContractItems { get; set; } = new List<PersonnelContractItem>();

    [ForeignKey("PersonnelFinancialInformationId")]
    [InverseProperty("PersonnelContracts")]
    public virtual PersonnelFinancialInformation PersonnelFinancialInformation { get; set; } = null!;

    [ForeignKey("PersonnelInsuranceId")]
    [InverseProperty("PersonnelContracts")]
    public virtual PersonnelInsurance PersonnelInsurance { get; set; } = null!;

    [ForeignKey("PersonnelJobId")]
    [InverseProperty("PersonnelContracts")]
    public virtual PersonnelJob PersonnelJob { get; set; } = null!;

    [InverseProperty("PersonnelContract")]
    public virtual ICollection<WorkRecord> WorkRecords { get; set; } = new List<WorkRecord>();
}
