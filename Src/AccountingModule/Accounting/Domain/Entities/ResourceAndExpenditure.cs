using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("resource_and_expenditures", Schema = "accounting")]
[Index("CompanyId", Name = "resource_and_expenditures_company_id_5d08b6e8")]
public partial class ResourceAndExpenditure
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [InverseProperty("ResourceAndExpenditure")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("DefaultResourceAndExpenditure")]
    public virtual ICollection<ClosingPatternSlaveCompany> ClosingPatternSlaveCompanies { get; set; } = new List<ClosingPatternSlaveCompany>();

    [InverseProperty("DefaultResourceAndExpenditure")]
    public virtual ICollection<ClosingPatternTemporaryAccount> ClosingPatternTemporaryAccounts { get; set; } = new List<ClosingPatternTemporaryAccount>();

    [InverseProperty("DefaultResourceAndExpenditure")]
    public virtual ICollection<ClosingPattern> ClosingPatterns { get; set; } = new List<ClosingPattern>();

    [InverseProperty("ResourceAndExpenditure")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ResourceAndExpenditure")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ResourceAndExpenditure")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();
}
