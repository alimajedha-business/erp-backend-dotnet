using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("trash_voucher_item_attaches", Schema = "accounting")]
[Index("TrashVoucherId", Name = "trash_voucher_item_attaches_trash_voucher_id_aea53052")]
[Index("TrashVoucherItemId", Name = "trash_voucher_item_attaches_trash_voucher_item_id_f86212d2")]
public partial class TrashVoucherItemAttach
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("file")]
    [StringLength(100)]
    public string File { get; set; } = null!;

    [Column("created_at")]
    public DateOnly CreatedAt { get; set; }

    [Column("trash_voucher_id")]
    public int TrashVoucherId { get; set; }

    [Column("trash_voucher_item_id")]
    public int TrashVoucherItemId { get; set; }

    [ForeignKey("TrashVoucherId")]
    [InverseProperty("TrashVoucherItemAttaches")]
    public virtual TrashVoucher TrashVoucher { get; set; } = null!;

    [ForeignKey("TrashVoucherItemId")]
    [InverseProperty("TrashVoucherItemAttaches")]
    public virtual TrashVoucherItem TrashVoucherItem { get; set; } = null!;
}
