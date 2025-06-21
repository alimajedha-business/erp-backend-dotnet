using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_importations", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_imp__A2D3D11B86124CAE", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_importations_arz_type_id_66a0f015")]
[Index("CompanyId", Name = "ttms_importations_company_id_d3aecf45")]
[Index("ForoshandeAddressId", Name = "ttms_importations_foroshande_address_id_c9332593")]
[Index("ForoshandeCountryId", Name = "ttms_importations_foroshande_country_id_7cf0dc0a")]
[Index("ForoshandeId", Name = "ttms_importations_foroshande_id_681a08be")]
[Index("KalaTypeId", Name = "ttms_importations_kala_type_id_cb9fffb1")]
[Index("LedgerId", Name = "ttms_importations_ledger_id_a8e87721")]
[Index("PeriodId", Name = "ttms_importations_period_id_e707fd86")]
[Index("VoucherId", Name = "ttms_importations_voucher_id_e6c6d27d")]
public partial class TtmsImportation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("kharid_type")]
    public short? KharidType { get; set; }

    [Column("bargasht_az_saderat")]
    public bool BargashtAzSaderat { get; set; }

    [Column("HC_foroushande_type_1_code")]
    public short? HcForoushandeType1Code { get; set; }

    [Column("kala_code", TypeName = "numeric(20, 0)")]
    public decimal? KalaCode { get; set; }

    [Column("kala_khadamat_name")]
    [StringLength(100)]
    public string? KalaKhadamatName { get; set; }

    [Column("price")]
    [StringLength(23)]
    public string? Price { get; set; }

    [Column("price_parvane")]
    [StringLength(23)]
    public string? PriceParvane { get; set; }

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

    [Column("lc_no")]
    [StringLength(19)]
    public string? LcNo { get; set; }

    [Column("lc_date")]
    [StringLength(10)]
    public string? LcDate { get; set; }

    [Column("gomrok_arzyabi")]
    public int? GomrokArzyabi { get; set; }

    [Column("gomrok_vorud")]
    public int? GomrokVorud { get; set; }

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

    [Column("foroshande_id")]
    public int? ForoshandeId { get; set; }

    [Column("foroshande_address_id")]
    public int? ForoshandeAddressId { get; set; }

    [Column("foroshande_country_id")]
    public int? ForoshandeCountryId { get; set; }

    [Column("kala_type_id")]
    [StringLength(10)]
    public string? KalaTypeId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("KalaTypeId")]
    [InverseProperty("TtmsImportations")]
    public virtual TtmsProductType? KalaType { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsImportations")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsImportations")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsImportations")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsImportation")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
