// Ignore Spelling: Ttms

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("vouchers", Schema = "accounting")]
[Index("BranchId", Name = "vouchers_branch_id_f6f281ae")]
[Index("CompanyId", Name = "vouchers_company_id_87927ee9")]
[Index("CreatorId", Name = "vouchers_creator_id_b7f0b4f8")]
[Index("LastModifierId", Name = "vouchers_last_modifier_id_61732409")]
[Index("LedgerId", Name = "vouchers_ledger_id_25e3f63d")]
[Index("ModuleId", Name = "vouchers_module_id_65b2cf76")]
[Index("PeriodId", Name = "vouchers_period_id_800ded6b")]
[Index("TypeId", Name = "vouchers_type_id_05bcc3a6")]
public partial class Voucher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_initial")]
    public int? IdInitial { get; set; }

    [Column("status")]
    [StringLength(9)]
    public string Status { get; set; } = null!;

    [Column("serial")]
    public long Serial { get; set; }

    [Column("number")]
    public long Number { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("last_modifier_id")]
    public int? LastModifierId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("type_id")]
    public int TypeId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("Vouchers")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("Vouchers")]
    public virtual Period Period { get; set; } = null!;

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsBuy> TtmsBuys { get; set; } = new List<TtmsBuy>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsContractorInfo> TtmsContractorInfos { get; set; } = new List<TtmsContractorInfo>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsEmployerInfo> TtmsEmployerInfos { get; set; } = new List<TtmsEmployerInfo>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsExportation> TtmsExportations { get; set; } = new List<TtmsExportation>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsImportation> TtmsImportations { get; set; } = new List<TtmsImportation>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsLeaseAgreement> TtmsLeaseAgreements { get; set; } = new List<TtmsLeaseAgreement>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsPreSell> TtmsPreSells { get; set; } = new List<TtmsPreSell>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsSell> TtmsSells { get; set; } = new List<TtmsSell>();

    [InverseProperty("Voucher")]
    public virtual ICollection<TtmsWage> TtmsWages { get; set; } = new List<TtmsWage>();

    [ForeignKey("TypeId")]
    [InverseProperty("Vouchers")]
    public virtual VoucherType Type { get; set; } = null!;

    [InverseProperty("Voucher")]
    public virtual ICollection<VoucherItemAttach> VoucherItemAttaches { get; set; } = new List<VoucherItemAttach>();

    [InverseProperty("Voucher")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("Voucher")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();

    [InverseProperty("Voucher")]
    public virtual ICollection<VoucherLog> VoucherLogs { get; set; } = new List<VoucherLog>();
}
