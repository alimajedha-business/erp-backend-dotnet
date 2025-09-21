using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Domain.Entities;

[Table("voucher_types", Schema = "accounting")]
[Index("NameFa", Name = "UQ__voucher___2E0A7AA62E206246", IsUnique = true)]
public partial class VoucherType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_static")]
    public bool IsStatic { get; set; }

    [Column("name_fa")]
    [StringLength(100)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(100)]
    public string? NameEn { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<TrashVoucher> TrashVouchers { get; set; } = new List<TrashVoucher>();

    [InverseProperty("OldVoucherType")]
    public virtual ICollection<VoucherLog> VoucherLogOldVoucherTypes { get; set; } = new List<VoucherLog>();

    [InverseProperty("VoucherType")]
    public virtual ICollection<VoucherLog> VoucherLogVoucherTypes { get; set; } = new List<VoucherLog>();

    [InverseProperty("Type")]
    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
