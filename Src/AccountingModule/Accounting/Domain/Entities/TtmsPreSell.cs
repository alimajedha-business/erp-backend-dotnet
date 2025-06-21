using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_pre_sells", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_pre__A2D3D11BC3C56D57", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_pre_sells_arz_type_id_141bbcfb")]
[Index("CompanyId", Name = "ttms_pre_sells_company_id_2a037b71")]
[Index("GharardadId", Name = "ttms_pre_sells_gharardad_id_c447c81b")]
[Index("LedgerId", Name = "ttms_pre_sells_ledger_id_e56cf010")]
[Index("MelkCityId", Name = "ttms_pre_sells_melk_city_id_83ae0854")]
[Index("MelkStateId", Name = "ttms_pre_sells_melk_state_id_b05f8d9c")]
[Index("PeriodId", Name = "ttms_pre_sells_period_id_d371cddf")]
[Index("TAddressId", Name = "ttms_pre_sells_t_address_id_c02594e3")]
[Index("TPhoneId", Name = "ttms_pre_sells_t_phone_id_97c78fc6")]
[Index("TarafeGharardadId", Name = "ttms_pre_sells_tarafe_gharardad_id_bcffbf4c")]
[Index("VoucherId", Name = "ttms_pre_sells_voucher_id_58f4d88f")]
public partial class TtmsPreSell
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tarafe_gharardad_type")]
    public short? TarafeGharardadType { get; set; }

    [Column("gharardad_type_code")]
    public short? GharardadTypeCode { get; set; }

    [Column("karbari_type")]
    public short? KarbariType { get; set; }

    [Column("tozih")]
    [StringLength(255)]
    public string? Tozih { get; set; }

    [Column("melk_address")]
    [StringLength(80)]
    public string? MelkAddress { get; set; }

    [Column("pelak_sabti")]
    [StringLength(25)]
    public string? PelakSabti { get; set; }

    [Column("bargasht_type")]
    public bool BargashtType { get; set; }

    [Column("pardakht_date")]
    [StringLength(10)]
    public string? PardakhtDate { get; set; }

    [Column("tozihat_pardakht")]
    [StringLength(255)]
    public string? TozihatPardakht { get; set; }

    [Column("price")]
    [StringLength(23)]
    public string Price { get; set; } = null!;

    [Column("arz_price")]
    [StringLength(23)]
    public string? ArzPrice { get; set; }

    [Column("arz_barabari_price")]
    [StringLength(23)]
    public string? ArzBarabariPrice { get; set; }

    [Column("moadel_riali_price")]
    [StringLength(23)]
    public string? MoadelRialiPrice { get; set; }

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

    [Column("t_address_id")]
    public int? TAddressId { get; set; }

    [Column("t_phone_id")]
    public int? TPhoneId { get; set; }

    [Column("tarafe_gharardad_id")]
    public int? TarafeGharardadId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsPreSells")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsPreSells")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsPreSells")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsPreSell")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
