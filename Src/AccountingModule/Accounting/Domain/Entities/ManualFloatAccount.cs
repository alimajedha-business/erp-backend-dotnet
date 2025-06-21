using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("manual_float_accounts", Schema = "accounting")]
[Index("CompanyId", Name = "manual_float_accounts_company_id_c4223ce2")]
[Index("FloatAccountTypeId", Name = "manual_float_accounts_float_account_type_id_b211d188")]
[Index("ParentId", Name = "manual_float_accounts_parent_id_4b9bca91")]
public partial class ManualFloatAccount
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

    [Column("description")]
    [StringLength(200)]
    public string? Description { get; set; }

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

    [Column("float_account_type_id")]
    public int FloatAccountTypeId { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("ManualFloat1")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat1s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat2")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat2s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat3")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat3s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat4")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat4s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat5")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat5s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat6")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat6s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat7")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat7s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("ManualFloat8")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetailManualFloat8s { get; set; } = new List<BalanceRelatedAccountDetail>();

    [ForeignKey("FloatAccountTypeId")]
    [InverseProperty("ManualFloatAccounts")]
    public virtual FloatAccountType FloatAccountType { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<ManualFloatAccount> InverseParent { get; set; } = new List<ManualFloatAccount>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ManualFloatAccount? Parent { get; set; }

    [InverseProperty("ManualFloat1")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat1s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat2")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat2s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat3")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat3s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat4")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat4s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat5")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat5s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat6")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat6s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat7")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat7s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat8")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItemManualFloat8s { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("ManualFloat1")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat1s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat2")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat2s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat3")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat3s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat4")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat4s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat5")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat5s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat6")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat6s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat7")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat7s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat8")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogManualFloat8s { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("ManualFloat1")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat1s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat2")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat2s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat3")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat3s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat4")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat4s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat5")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat5s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat6")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat6s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat7")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat7s { get; set; } = new List<VoucherItem>();

    [InverseProperty("ManualFloat8")]
    public virtual ICollection<VoucherItem> VoucherItemManualFloat8s { get; set; } = new List<VoucherItem>();
}
