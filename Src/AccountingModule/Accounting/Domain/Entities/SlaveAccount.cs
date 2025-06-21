using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("slave_accounts", Schema = "accounting")]
[Index("CategoryId", Name = "slave_accounts_category_id_79276cde")]
[Index("FromCompanyIdId", Name = "slave_accounts_from_company_id_id_be582ecc")]
[Index("GroupId", Name = "slave_accounts_group_id_d226c356")]
[Index("LedgerId", Name = "slave_accounts_ledger_id_e9bba48c")]
[Index("MasterId", Name = "slave_accounts_master_id_d8364647")]
[Index("ParentId", Name = "slave_accounts_parent_id_b5e3fe05")]
public partial class SlaveAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    [StringLength(13)]
    public string Type { get; set; } = null!;

    [Column("nature")]
    [StringLength(8)]
    public string Nature { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("detailed_account_1")]
    public int? DetailedAccount1 { get; set; }

    [Column("detailed_account_2")]
    public int? DetailedAccount2 { get; set; }

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

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("from_company_id_id")]
    public int? FromCompanyIdId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("closing_type")]
    [StringLength(13)]
    public string ClosingType { get; set; } = null!;

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [Column("last_level")]
    public bool LastLevel { get; set; }

    [Column("level")]
    public short Level { get; set; }

    [Column("slave_code")]
    public int SlaveCode { get; set; }

    [InverseProperty("Slave")]
    public virtual ICollection<AccountSetItem> AccountSetItems { get; set; } = new List<AccountSetItem>();

    [InverseProperty("Slave")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; } = new List<BalanceRelatedAccountDetail>();

    [ForeignKey("CategoryId")]
    [InverseProperty("SlaveAccounts")]
    public virtual AccountCategory Category { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("SlaveAccounts")]
    public virtual AccountGroup Group { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<SlaveAccount> InverseParent { get; set; } = new List<SlaveAccount>();

    [ForeignKey("LedgerId")]
    [InverseProperty("SlaveAccounts")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("SlaveAccounts")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual SlaveAccount? Parent { get; set; }

    [InverseProperty("Slave")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanies { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("Slave")]
    public virtual ICollection<SlaveAccountStandardDescription> SlaveAccountStandardDescriptions { get; set; } = new List<SlaveAccountStandardDescription>();

    [InverseProperty("Slave")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("Slave")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("Slave")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();
}
