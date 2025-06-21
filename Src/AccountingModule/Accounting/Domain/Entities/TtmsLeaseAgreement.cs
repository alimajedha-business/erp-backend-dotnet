using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_lease_agreements", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_lea__A2D3D11BF7748A1E", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_lease_agreements_arz_type_id_eb4b0083")]
[Index("CompanyId", Name = "ttms_lease_agreements_company_id_fe87aa2f")]
[Index("GharardadId", Name = "ttms_lease_agreements_gharardad_id_9c794542")]
[Index("LedgerId", Name = "ttms_lease_agreements_ledger_id_fbcd73fe")]
[Index("MelkCityId", Name = "ttms_lease_agreements_melk_city_id_c6850824")]
[Index("MelkStateId", Name = "ttms_lease_agreements_melk_state_id_970e540d")]
[Index("PeriodId", Name = "ttms_lease_agreements_period_id_b7dd6e0b")]
[Index("TarafeGharardadAddressId", Name = "ttms_lease_agreements_tarafe_gharardad_address_id_cf429de4")]
[Index("TarafeGharardadId", Name = "ttms_lease_agreements_tarafe_gharardad_id_9d1d1f02")]
[Index("TarafeGharardadPhoneId", Name = "ttms_lease_agreements_tarafe_gharardad_phone_id_c4fefd27")]
[Index("VoucherId", Name = "ttms_lease_agreements_voucher_id_38801563")]
public partial class TtmsLeaseAgreement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tarafe_gharardad_type")]
    public short? TarafeGharardadType { get; set; }

    [Column("tedad_shoraka")]
    public short? TedadShoraka { get; set; }

    [Column("gharardad_type_code")]
    public short? GharardadTypeCode { get; set; }

    [Column("ejari_type")]
    public short? EjariType { get; set; }

    [Column("tozih")]
    [StringLength(255)]
    public string? Tozih { get; set; }

    [Column("vasile")]
    public short? Vasile { get; set; }

    [Column("karbari_type")]
    public short? KarbariType { get; set; }

    [Column("melk_address")]
    [StringLength(80)]
    public string? MelkAddress { get; set; }

    [Column("melk_post_code")]
    [StringLength(10)]
    public string? MelkPostCode { get; set; }

    [Column("melk_tell_pish_code")]
    [StringLength(10)]
    public string? MelkTellPishCode { get; set; }

    [Column("melk_tell")]
    [StringLength(50)]
    public string? MelkTell { get; set; }

    [Column("pelak_sabti_asli")]
    [StringLength(25)]
    public string? PelakSabtiAsli { get; set; }

    [Column("pelak_sabti_fari")]
    [StringLength(25)]
    public string? PelakSabtiFari { get; set; }

    [Column("bakhsh_sabti")]
    [StringLength(25)]
    public string? BakhshSabti { get; set; }

    [Column("shenase_melk")]
    [StringLength(25)]
    public string? ShenaseMelk { get; set; }

    [Column("mozu_type")]
    public short? MozuType { get; set; }

    [Column("nakhales")]
    [StringLength(23)]
    public string? Nakhales { get; set; }

    [Column("maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MaliatArzeshAfzoodeh { get; set; }

    [Column("avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? AvarezArzeshAfzoodeh { get; set; }

    [Column("maliat_maksoore")]
    [StringLength(23)]
    public string? MaliatMaksoore { get; set; }

    [Column("arz_nakhales")]
    [StringLength(23)]
    public string? ArzNakhales { get; set; }

    [Column("arz_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_maliat_maksoore")]
    [StringLength(23)]
    public string? ArzMaliatMaksoore { get; set; }

    [Column("arz_barabari_nakhales", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariNakhales { get; set; }

    [Column("arz_barabari_maliat_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_avarez_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_maliat_maksoore", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatMaksoore { get; set; }

    [Column("moadel_riali_nakhales")]
    [StringLength(23)]
    public string? MoadelRialiNakhales { get; set; }

    [Column("moadel_riali_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiMaliatArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiAvarezArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_maliat_maksoore")]
    [StringLength(23)]
    public string? MoadelRialiMaliatMaksoore { get; set; }

    [Column("sanad_no")]
    public int? SanadNo { get; set; }

    [Column("sanad_date")]
    [StringLength(10)]
    public string? SanadDate { get; set; }

    [Column("deal_currency")]
    public short? DealCurrency { get; set; }

    [Column("arz_type_id")]
    public int? ArzTypeId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("gharardad_id")]
    public int? GharardadId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("melk_city_id")]
    public int? MelkCityId { get; set; }

    [Column("melk_state_id")]
    public int? MelkStateId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("tarafe_gharardad_id")]
    public int? TarafeGharardadId { get; set; }

    [Column("tarafe_gharardad_address_id")]
    public int? TarafeGharardadAddressId { get; set; }

    [Column("tarafe_gharardad_phone_id")]
    public int? TarafeGharardadPhoneId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsLeaseAgreements")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsLeaseAgreements")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsLeaseAgreements")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsLeaseAgreement")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
