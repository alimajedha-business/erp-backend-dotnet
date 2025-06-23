using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("tax_workshops", Schema = "payroll")]
[Index("CompanyId", Name = "tax_workshops_company_id_9b839770")]
[Index("CreatorId", Name = "tax_workshops_creator_id_50451733")]
public partial class TaxWorkshop
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("taxpayer_type")]
    [StringLength(7)]
    public string TaxpayerType { get; set; } = null!;

    [Column("branch_number")]
    [StringLength(50)]
    public string? BranchNumber { get; set; }

    [Column("branch")]
    [StringLength(50)]
    public string? Branch { get; set; }

    [Column("economic_code")]
    [StringLength(14)]
    public string? EconomicCode { get; set; }

    [Column("national_id")]
    [StringLength(14)]
    public string? NationalId { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

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

    [InverseProperty("TaxWorkshop")]
    public virtual ICollection<PersonnelJob> PersonnelJobs { get; set; } = new List<PersonnelJob>();
}
