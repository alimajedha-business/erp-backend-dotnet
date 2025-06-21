using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("voucher_item_attaches", Schema = "accounting")]
[Index("VoucherId", Name = "voucher_item_attaches_voucher_id_05221da5")]
[Index("VoucherItemId", Name = "voucher_item_attaches_voucher_item_id_3f1216ab")]
public partial class VoucherItemAttach
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("file")]
    [StringLength(100)]
    public string File { get; set; } = null!;

    [Column("created_at")]
    public DateOnly CreatedAt { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("VoucherId")]
    [InverseProperty("VoucherItemAttaches")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("VoucherItemAttaches")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
