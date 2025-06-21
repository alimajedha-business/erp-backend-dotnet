using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("master_accounts", Schema = "accounting")]
[Index("LedgerId", Name = "master_accounts_ledger_id_b94026a2")]
public partial class MasterAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("title2")]
    [StringLength(100)]
    public string? Title2 { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [InverseProperty("Master")]
    public virtual ICollection<AccountSetItem> AccountSetItems { get; set; } = new List<AccountSetItem>();

    [InverseProperty("Master")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; } = new List<BalanceRelatedAccountDetail>();

    [ForeignKey("LedgerId")]
    [InverseProperty("MasterAccounts")]
    public virtual Ledger Ledger { get; set; } = null!;

    [InverseProperty("Master")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanies { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("Master")]
    public virtual ICollection<SlaveAccount> SlaveAccounts { get; set; } = new List<SlaveAccount>();

    [InverseProperty("Master")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("Master")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("Master")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();
}
