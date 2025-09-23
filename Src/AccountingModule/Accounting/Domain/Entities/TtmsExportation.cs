using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_exportations", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_exp__A2D3D11B14C08510", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_exportations_arz_type_id_a88c4e64")]
[Index("CompanyId", Name = "ttms_exportations_company_id_d4e9b554")]
[Index("KalaTypeId", Name = "ttms_exportations_kala_type_id_c8a03da3")]
[Index("KharidarAddressId", Name = "ttms_exportations_kharidar_address_id_b43af6ed")]
[Index("KharidarCountryId", Name = "ttms_exportations_kharidar_country_id_5b8e0edc")]
[Index("KharidarId", Name = "ttms_exportations_kharidar_id_f33336bf")]
[Index("LedgerId", Name = "ttms_exportations_ledger_id_b7fff67d")]
[Index("PeriodId", Name = "ttms_exportations_period_id_7c7cf71b")]
[Index("VoucherId", Name = "ttms_exportations_voucher_id_14d3710f")]
public partial class TtmsExportation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("foroush_type")]
    public short? ForoushType { get; set; }

    [Column("hc_kharidar_type_1_code")]
    public short? HcKharidarType1Code { get; set; }

    [Column("kala_khadamat_name")]
    [StringLength(100)]
    public string? KalaKhadamatName { get; set; }

    [Column("kala_code", TypeName = "numeric(20, 0)")]
    public decimal? KalaCode { get; set; }

    [Column("price")]
    [StringLength(23)]
    public string Price { get; set; } = null!;

    [Column("price_parvane")]
    [StringLength(23)]
    public string? PriceParvane { get; set; }

    [Column("maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string MaliatArzeshAfzoodeh { get; set; } = null!;

    [Column("avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string AvarezArzeshAfzoodeh { get; set; } = null!;

    [Column("sayer_avarez")]
    [StringLength(23)]
    public string SayerAvarez { get; set; } = null!;

    [Column("takhfif_price")]
    [StringLength(23)]
    public string? TakhfifPrice { get; set; }

    [Column("maliat_maksoore")]
    [StringLength(23)]
    public string? MaliatMaksoore { get; set; }

    [Column("arz_price")]
    [StringLength(23)]
    public string? ArzPrice { get; set; }

    [Column("arz_price_parvane")]
    [StringLength(23)]
    public string? ArzPriceParvane { get; set; }

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

    [Column("arz_barabari_price_parvane", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariPriceParvane { get; set; }

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

    [Column("moadel_riali_price_parvane")]
    [StringLength(23)]
    public string? MoadelRialiPriceParvane { get; set; }

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

    [Column("kotaj_no")]
    public int? KotajNo { get; set; }

    [Column("kotaj_date")]
    [StringLength(10)]
    public string? KotajDate { get; set; }

    [Column("LC_no")]
    [StringLength(19)]
    public string? LcNo { get; set; }

    [Column("LC_date")]
    [StringLength(10)]
    public string? LcDate { get; set; }

    [Column("gomrok_arzyabi")]
    public int? GomrokArzyabi { get; set; }

    [Column("gomrok_khoruj")]
    public int? GomrokKhoruj { get; set; }

    [Column("factor_no")]
    [StringLength(50)]
    public string? FactorNo { get; set; }

    [Column("factor_date")]
    [StringLength(10)]
    public string? FactorDate { get; set; }

    [Column("tozih")]
    [StringLength(255)]
    public string? Tozih { get; set; }

    [Column("deal_currency")]
    public short? DealCurrency { get; set; }

    [Column("arz_type_id")]
    public int? ArzTypeId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("kala_type_id")]
    [StringLength(10)]
    public int? KalaTypeId { get; set; }

    [Column("kharidar_id")]
    public int? KharidarId { get; set; }

    [Column("kharidar_address_id")]
    public int? KharidarAddressId { get; set; }

    [Column("kharidar_country_id")]
    public int? KharidarCountryId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("KalaTypeId")]
    [InverseProperty("TtmsExportations")]
    public virtual TtmsProductType? KalaType { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsExportations")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsExportations")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsExportations")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsExportation")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
