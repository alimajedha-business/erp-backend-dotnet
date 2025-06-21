using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_sells", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_sel__A2D3D11B5AFD5124", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_sells_arz_type_id_6035a895")]
[Index("CompanyId", Name = "ttms_sells_company_id_f2b729b6")]
[Index("KalaTypeId", Name = "ttms_sells_kala_type_id_49581b94")]
[Index("KharidarAddressId", Name = "ttms_sells_kharidar_address_id_8e5898be")]
[Index("KharidarId", Name = "ttms_sells_kharidar_id_28d5fad0")]
[Index("KharidarPhoneId", Name = "ttms_sells_kharidar_phone_id_00e26fff")]
[Index("LedgerId", Name = "ttms_sells_ledger_id_e8e19e1e")]
[Index("PeriodId", Name = "ttms_sells_period_id_bdec3655")]
[Index("VoucherId", Name = "ttms_sells_voucher_id_96da188f")]
public partial class TtmsSell
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sarjam")]
    public bool Sarjam { get; set; }

    [Column("is_hagholamal_kari")]
    public bool IsHagholamalKari { get; set; }

    [Column("kala_khadamat_name")]
    [StringLength(100)]
    public string? KalaKhadamatName { get; set; }

    [Column("kala_code", TypeName = "numeric(20, 0)")]
    public decimal? KalaCode { get; set; }

    [Column("bargasht_type")]
    public bool BargashtType { get; set; }

    [Column("price")]
    [StringLength(23)]
    public string? Price { get; set; }

    [Column("maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MaliatArzeshAfzoodeh { get; set; }

    [Column("avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? AvarezArzeshAfzoodeh { get; set; }

    [Column("sayer_avarez")]
    [StringLength(23)]
    public string? SayerAvarez { get; set; }

    [Column("takhfif_price")]
    [StringLength(23)]
    public string? TakhfifPrice { get; set; }

    [Column("maliat_maksoore")]
    [StringLength(23)]
    public string? MaliatMaksoore { get; set; }

    [Column("arz_price")]
    [StringLength(23)]
    public string? ArzPrice { get; set; }

    [Column("arz_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_sayer_avarez")]
    [StringLength(23)]
    public string? ArzSayerAvarez { get; set; }

    [Column("arz_takhfif_price")]
    [StringLength(23)]
    public string? ArzTakhfifPrice { get; set; }

    [Column("arz_maliat_maksoore")]
    [StringLength(23)]
    public string? ArzMaliatMaksoore { get; set; }

    [Column("arz_barabari_price", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariPrice { get; set; }

    [Column("arz_barabari_takhfif_price", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariTakhfifPrice { get; set; }

    [Column("arz_barabari_maliat_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_avarez_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_sayer_avarez", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariSayerAvarez { get; set; }

    [Column("arz_barabari_maliat_maksoore", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatMaksoore { get; set; }

    [Column("moadel_riali_price")]
    [StringLength(23)]
    public string? MoadelRialiPrice { get; set; }

    [Column("moadel_riali_takhfif_price")]
    [StringLength(23)]
    public string? MoadelRialiTakhfifPrice { get; set; }

    [Column("moadel_riali_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiMaliatArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiAvarezArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_sayer_avarez")]
    [StringLength(23)]
    public string? MoadelRialiSayerAvarez { get; set; }

    [Column("moadel_riali_maliat_maksoore")]
    [StringLength(23)]
    public string? MoadelRialiMaliatMaksoore { get; set; }

    [Column("factor_no")]
    [StringLength(50)]
    public string? FactorNo { get; set; }

    [Column("factor_date")]
    [StringLength(10)]
    public string? FactorDate { get; set; }

    [Column("deal_currency")]
    public short? DealCurrency { get; set; }

    [Column("arz_type_id")]
    public int? ArzTypeId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("kala_type_id")]
    [StringLength(10)]
    public string? KalaTypeId { get; set; }

    [Column("kharidar_id")]
    public int? KharidarId { get; set; }

    [Column("kharidar_address_id")]
    public int? KharidarAddressId { get; set; }

    [Column("kharidar_phone_id")]
    public int? KharidarPhoneId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("KalaTypeId")]
    [InverseProperty("TtmsSells")]
    public virtual TtmsProductType? KalaType { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsSells")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsSells")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsSells")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsSell")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
