using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_wages", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_wag__A2D3D11BC43A0393", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_wages_arz_type_id_aefc4dc1")]
[Index("CompanyId", Name = "ttms_wages_company_id_fbb2bfe2")]
[Index("HArzTypeId", Name = "ttms_wages_h_arz_type_id_fc57f533")]
[Index("KalaTypeId", Name = "ttms_wages_kala_type_id_8ee391b0")]
[Index("KharidarAddressId", Name = "ttms_wages_kharidar_address_id_e43b08df")]
[Index("KharidarId", Name = "ttms_wages_kharidar_id_78670155")]
[Index("KharidarPhoneId", Name = "ttms_wages_kharidar_phone_id_11a031a9")]
[Index("LedgerId", Name = "ttms_wages_ledger_id_2e5fbf4c")]
[Index("PeriodId", Name = "ttms_wages_period_id_7e023cdd")]
[Index("SellerAddressId", Name = "ttms_wages_seller_address_id_ee67712a")]
[Index("SellerId", Name = "ttms_wages_seller_id_a18419a3")]
[Index("SellerPhoneId", Name = "ttms_wages_seller_phone_id_985624b3")]
[Index("VoucherId", Name = "ttms_wages_voucher_id_ebeeee11")]
public partial class TtmsWage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_type")]
    public short? ReportType { get; set; }

    [Column("daryaft_az")]
    public short? DaryaftAz { get; set; }

    [Column("sarjam")]
    public bool Sarjam { get; set; }

    [Column("bargasht_type")]
    public bool BargashtType { get; set; }

    [Column("s_type_1_code")]
    public short? SType1Code { get; set; }

    [Column("k_type_1_code")]
    public short? KType1Code { get; set; }

    [Column("h_nerkh")]
    [StringLength(23)]
    public string? HNerkh { get; set; }

    [Column("h_price")]
    [StringLength(23)]
    public string? HPrice { get; set; }

    [Column("h_takhfif_price")]
    [StringLength(23)]
    public string? HTakhfifPrice { get; set; }

    [Column("h_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HMaliatArzeshAfzoodeh { get; set; }

    [Column("h_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HAvarezArzeshAfzoodeh { get; set; }

    [Column("h_sayer_avarez")]
    [StringLength(23)]
    public string? HSayerAvarez { get; set; }

    [Column("h_arz_price")]
    [StringLength(23)]
    public string? HArzPrice { get; set; }

    [Column("h_arz_takhfif_price")]
    [StringLength(23)]
    public string? HArzTakhfifPrice { get; set; }

    [Column("h_arz_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HArzMaliatArzeshAfzoodeh { get; set; }

    [Column("h_arz_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HArzAvarezArzeshAfzoodeh { get; set; }

    [Column("h_arz_sayer_avarez")]
    [StringLength(23)]
    public string? HArzSayerAvarez { get; set; }

    [Column("h_arz_barabari_price", TypeName = "numeric(13, 5)")]
    public decimal? HArzBarabariPrice { get; set; }

    [Column("h_arz_barabari_takhfif_price", TypeName = "numeric(13, 5)")]
    public decimal? HArzBarabariTakhfifPrice { get; set; }

    [Column("h_arz_barabari_maliat_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? HArzBarabariMaliatArzeshAfzoodeh { get; set; }

    [Column("h_arz_barabari_avarez_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? HArzBarabariAvarezArzeshAfzoodeh { get; set; }

    [Column("h_arz_barabari_sayer_avarez", TypeName = "numeric(13, 5)")]
    public decimal? HArzBarabariSayerAvarez { get; set; }

    [Column("h_moadel_riali_price")]
    [StringLength(23)]
    public string? HMoadelRialiPrice { get; set; }

    [Column("h_moadel_riali_takhfif_price")]
    [StringLength(23)]
    public string? HMoadelRialiTakhfifPrice { get; set; }

    [Column("h_moadel_riali_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HMoadelRialiMaliatArzeshAfzoodeh { get; set; }

    [Column("h_moadel_riali_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? HMoadelRialiAvarezArzeshAfzoodeh { get; set; }

    [Column("h_moadel_riali_sayer_avarez")]
    [StringLength(23)]
    public string? HMoadelRialiSayerAvarez { get; set; }

    [Column("h_factor_no")]
    [StringLength(50)]
    public string? HFactorNo { get; set; }

    [Column("h_factor_date")]
    [StringLength(10)]
    public string? HFactorDate { get; set; }

    [Column("kala_khadamat_name")]
    [StringLength(100)]
    public string? KalaKhadamatName { get; set; }

    [Column("kala_code", TypeName = "numeric(20, 0)")]
    public decimal? KalaCode { get; set; }

    [Column("price")]
    [StringLength(23)]
    public string? Price { get; set; }

    [Column("takhfif_price")]
    [StringLength(23)]
    public string? TakhfifPrice { get; set; }

    [Column("maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MaliatArzeshAfzoodeh { get; set; }

    [Column("avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? AvarezArzeshAfzoodeh { get; set; }

    [Column("sayer_avarez")]
    [StringLength(23)]
    public string? SayerAvarez { get; set; }

    [Column("arz_price")]
    [StringLength(23)]
    public string? ArzPrice { get; set; }

    [Column("arz_takhfif_price")]
    [StringLength(23)]
    public string? ArzTakhfifPrice { get; set; }

    [Column("arz_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_sayer_avarez")]
    [StringLength(23)]
    public string? ArzSayerAvarez { get; set; }

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

    [Column("h_arz_type_id")]
    public int? HArzTypeId { get; set; }

    [Column("kala_type_id")]
    public int? KalaTypeId { get; set; }

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

    [Column("seller_id")]
    public int? SellerId { get; set; }

    [Column("seller_address_id")]
    public int? SellerAddressId { get; set; }

    [Column("seller_phone_id")]
    public int? SellerPhoneId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("KalaTypeId")]
    [InverseProperty("TtmsWages")]
    public virtual TtmsProductType? KalaType { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsWages")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsWages")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsWages")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsWage")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
