using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("trash_vouchers", Schema = "accounting")]
[Index("BranchId", Name = "trash_vouchers_branch_id_4da4e4fa")]
[Index("CompanyId", Name = "trash_vouchers_company_id_01f6a6f1")]
[Index("CreatorId", Name = "trash_vouchers_creator_id_50cfb8a4")]
[Index("LastModifierId", Name = "trash_vouchers_last_modifier_id_35d033e0")]
[Index("LedgerId", Name = "trash_vouchers_ledger_id_e6d73246")]
[Index("ModuleId", Name = "trash_vouchers_module_id_3481836f")]
[Index("PeriodId", Name = "trash_vouchers_period_id_f1b414ea")]
[Index("TypeId", Name = "trash_vouchers_type_id_62c0b2ab")]
[Index("VoucherIdInitial", Name = "trash_vouchers_voucher_id_initial_f5cefa98")]
public partial class TrashVoucher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("voucher_id_initial")]
    public int VoucherIdInitial { get; set; }

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
    [InverseProperty("TrashVouchers")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TrashVouchers")]
    public virtual Period Period { get; set; } = null!;

    [InverseProperty("TrashVoucher")]
    public virtual ICollection<TrashVoucherItemAttach> TrashVoucherItemAttaches { get; set; } = new List<TrashVoucherItemAttach>();

    [InverseProperty("TrashVoucher")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [ForeignKey("TypeId")]
    [InverseProperty("TrashVouchers")]
    public virtual VoucherType Type { get; set; } = null!;
}
