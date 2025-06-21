using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_buys", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_buy__A2D3D11B3AE62649", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_buys_arz_type_id_842752a5")]
[Index("CompanyId", Name = "ttms_buys_company_id_86d0df71")]
[Index("ForoshandeAddressId", Name = "ttms_buys_foroshande_address_id_f6676def")]
[Index("ForoshandeId", Name = "ttms_buys_foroshande_id_899255d3")]
[Index("ForoshandePhoneId", Name = "ttms_buys_foroshande_phone_id_85d73354")]
[Index("KalaTypeId", Name = "ttms_buys_kala_type_id_acc1c01e")]
[Index("LedgerId", Name = "ttms_buys_ledger_id_4592b702")]
[Index("PeriodId", Name = "ttms_buys_period_id_b172660c")]
[Index("VoucherId", Name = "ttms_buys_voucher_id_932f90cb")]
public partial class TtmsBuy
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

    [Column("foroshande_id")]
    public int? ForoshandeId { get; set; }

    [Column("foroshande_address_id")]
    public int? ForoshandeAddressId { get; set; }

    [Column("foroshande_phone_id")]
    public int? ForoshandePhoneId { get; set; }

    [Column("kala_type_id")]
    public int? KalaTypeId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("KalaTypeId")]
    [InverseProperty("TtmsBuys")]
    public virtual TtmsProductType? KalaType { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsBuys")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsBuys")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsBuys")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsBuy")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
