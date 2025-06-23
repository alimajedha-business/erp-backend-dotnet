using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("bank_branches", Schema = "shared")]
[Index("BankId", Name = "bank_branches_bank_id_5eff2bac")]
[Index("CompanyId", Name = "bank_branches_company_id_4a52d712")]
public partial class BankBranch
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("branch_number")]
    public int? BranchNumber { get; set; }

    [Column("phone")]
    [StringLength(100)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(200)]
    public string? Address { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("bank_id")]
    public int BankId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [InverseProperty("BankBranch")]
    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
}
