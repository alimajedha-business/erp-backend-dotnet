using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("employment_contract_templates", Schema = "payroll")]
[Index("CompanyId", Name = "employment_contract_templates_company_id_9f976e17")]
[Index("CreatorId", Name = "employment_contract_templates_creator_id_087ed952")]
[Index("EmploymentGroupId", Name = "employment_contract_templates_employment_group_id_7b23ef4f")]
public partial class EmploymentContractTemplate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

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

    [InverseProperty("EmploymentContractTemplate")]
    public virtual ICollection<EmploymentContractTemplateItem> EmploymentContractTemplateItems { get; set; } = new List<EmploymentContractTemplateItem>();

    [ForeignKey("EmploymentGroupId")]
    [InverseProperty("EmploymentContractTemplates")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;

    [InverseProperty("EmploymentContractTemplate")]
    public virtual ICollection<PersonnelContract> PersonnelContracts { get; set; } = new List<PersonnelContract>();
}
