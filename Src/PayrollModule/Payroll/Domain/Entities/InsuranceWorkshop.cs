using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("insurance_workshops", Schema = "payroll")]
[Index("CompanyId", Name = "insurance_workshops_company_id_8470a3f3")]
[Index("CreatorId", Name = "insurance_workshops_creator_id_91c0968e")]
public partial class InsuranceWorkshop
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("contract_number")]
    [StringLength(50)]
    public string? ContractNumber { get; set; }

    [Column("employer_name")]
    [StringLength(100)]
    public string? EmployerName { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("branch")]
    [StringLength(50)]
    public string Branch { get; set; } = null!;

    [Column("insurance_rate", TypeName = "numeric(5, 2)")]
    public decimal? InsuranceRate { get; set; }

    [Column("disk_type")]
    [StringLength(8)]
    public string DiskType { get; set; } = null!;

    [Column("employer_share_exemption_count")]
    public short EmployerShareExemptionCount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [InverseProperty("InsuranceWorkshop")]
    public virtual ICollection<PersonnelInsurance> PersonnelInsurances { get; set; } = new List<PersonnelInsurance>();
}
