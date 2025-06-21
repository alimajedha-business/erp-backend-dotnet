using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("voucher_logs", Schema = "accounting")]
[Index("BranchId", Name = "voucher_logs_branch_id_099c72df")]
[Index("CompanyId", Name = "voucher_logs_company_id_9a183a61")]
[Index("LedgerId", Name = "voucher_logs_ledger_id_b35b1cc4")]
[Index("ModifierId", Name = "voucher_logs_modifier_id_b948dfef")]
[Index("OldBranchId", Name = "voucher_logs_old_branch_id_fbd37c8c")]
[Index("OldVoucherTypeId", Name = "voucher_logs_old_voucher_type_id_e9551d26")]
[Index("PeriodId", Name = "voucher_logs_period_id_02857e11")]
[Index("VoucherId", Name = "voucher_logs_voucher_id_34970cb1")]
[Index("VoucherIdInitial", Name = "voucher_logs_voucher_id_initial_60e0fc0b")]
[Index("VoucherTypeId", Name = "voucher_logs_voucher_type_id_5ba5b90d")]
public partial class VoucherLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("voucher_id_initial")]
    public int? VoucherIdInitial { get; set; }

    [Column("typ")]
    [StringLength(35)]
    public string Typ { get; set; } = null!;

    [Column("reason")]
    [StringLength(500)]
    public string? Reason { get; set; }

    [Column("serial")]
    public long? Serial { get; set; }

    [Column("number")]
    public long? Number { get; set; }

    [Column("old_number")]
    public long? OldNumber { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }

    [Column("old_date")]
    public DateOnly? OldDate { get; set; }

    [Column("status")]
    [StringLength(9)]
    public string? Status { get; set; }

    [Column("old_status")]
    [StringLength(9)]
    public string? OldStatus { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("old_description")]
    [StringLength(500)]
    public string? OldDescription { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("branch_id")]
    public int? BranchId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("modifier_id")]
    public int ModifierId { get; set; }

    [Column("old_branch_id")]
    public int? OldBranchId { get; set; }

    [Column("old_voucher_type_id")]
    public int? OldVoucherTypeId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int? VoucherId { get; set; }

    [Column("voucher_type_id")]
    public int? VoucherTypeId { get; set; }

    [Column("data")]
    public string? Data { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("VoucherLogs")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("OldVoucherTypeId")]
    [InverseProperty("VoucherLogOldVoucherTypes")]
    public virtual VoucherType? OldVoucherType { get; set; }

    [ForeignKey("PeriodId")]
    [InverseProperty("VoucherLogs")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("VoucherLogs")]
    public virtual Voucher? Voucher { get; set; }

    [InverseProperty("VoucherLog")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [ForeignKey("VoucherTypeId")]
    [InverseProperty("VoucherLogVoucherTypes")]
    public virtual VoucherType? VoucherType { get; set; }
}
