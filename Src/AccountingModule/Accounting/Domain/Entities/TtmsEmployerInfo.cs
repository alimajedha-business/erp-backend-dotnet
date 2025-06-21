using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_employer_infos", Schema = "accounting")]
[Index("VoucherItemId", Name = "UQ__ttms_emp__A2D3D11B5AB028CE", IsUnique = true)]
[Index("ArzTypeId", Name = "ttms_employer_infos_arz_type_id_64a2746a")]
[Index("CompanyId", Name = "ttms_employer_infos_company_id_f5ced1a0")]
[Index("GharardadId", Name = "ttms_employer_infos_gharardad_id_38dc7897")]
[Index("KarfarmaAddressId", Name = "ttms_employer_infos_karfarma_address_id_fbf74ad6")]
[Index("KarfarmaId", Name = "ttms_employer_infos_karfarma_id_e737c2aa")]
[Index("KarfarmaPhoneId", Name = "ttms_employer_infos_karfarma_phone_id_a109c622")]
[Index("LedgerId", Name = "ttms_employer_infos_ledger_id_4e6cb6c7")]
[Index("PeriodId", Name = "ttms_employer_infos_period_id_f2de7f69")]
[Index("VoucherId", Name = "ttms_employer_infos_voucher_id_c3d5dd84")]
public partial class TtmsEmployerInfo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_type")]
    public short? ReportType { get; set; }

    [Column("bargasht_type")]
    public bool BargashtType { get; set; }

    [Column("total_price")]
    [StringLength(23)]
    public string? TotalPrice { get; set; }

    [Column("surat_vaziat_number")]
    [StringLength(50)]
    public string? SuratVaziatNumber { get; set; }

    [Column("surat_vaziat_date")]
    [StringLength(10)]
    public string? SuratVaziatDate { get; set; }

    [Column("daryaft_date")]
    [StringLength(10)]
    public string? DaryaftDate { get; set; }

    [Column("nakhales")]
    [StringLength(23)]
    public string? Nakhales { get; set; }

    [Column("khales_price")]
    [StringLength(23)]
    public string? KhalesPrice { get; set; }

    [Column("maliat_maksooreh")]
    [StringLength(23)]
    public string? MaliatMaksooreh { get; set; }

    [Column("maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MaliatArzeshAfzoodeh { get; set; }

    [Column("avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? AvarezArzeshAfzoodeh { get; set; }

    [Column("sayer_avarez")]
    [StringLength(23)]
    public string? SayerAvarez { get; set; }

    [Column("arz_total_price")]
    [StringLength(23)]
    public string? ArzTotalPrice { get; set; }

    [Column("arz_nakhales")]
    [StringLength(23)]
    public string? ArzNakhales { get; set; }

    [Column("arz_khales")]
    [StringLength(23)]
    public string? ArzKhales { get; set; }

    [Column("arz_maliat_maksooreh")]
    [StringLength(23)]
    public string? ArzMaliatMaksooreh { get; set; }

    [Column("arz_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? ArzAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_sayer_avarez")]
    [StringLength(23)]
    public string? ArzSayerAvarez { get; set; }

    [Column("arz_barabari_nakhales", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariNakhales { get; set; }

    [Column("arz_barabari_khales", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariKhales { get; set; }

    [Column("arz_barabari_maliat_maksooreh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatMaksooreh { get; set; }

    [Column("arz_barabari_maliat_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariMaliatArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_avarez_arzesh_afzoodeh", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariAvarezArzeshAfzoodeh { get; set; }

    [Column("arz_barabari_sayer_avarez", TypeName = "numeric(13, 5)")]
    public decimal? ArzBarabariSayerAvarez { get; set; }

    [Column("gharardad_activity_type_code")]
    public short? GharardadActivityTypeCode { get; set; }

    [Column("moadel_riali_nakhales")]
    [StringLength(23)]
    public string? MoadelRialiNakhales { get; set; }

    [Column("moadel_riali_maksooreh")]
    [StringLength(23)]
    public string? MoadelRialiMaksooreh { get; set; }

    [Column("moadel_riali_khales")]
    [StringLength(23)]
    public string? MoadelRialiKhales { get; set; }

    [Column("moadel_riali_maliat_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiMaliatArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_avarez_arzesh_afzoodeh")]
    [StringLength(23)]
    public string? MoadelRialiAvarezArzeshAfzoodeh { get; set; }

    [Column("moadel_riali_sayer_avarez")]
    [StringLength(23)]
    public string? MoadelRialiSayerAvarez { get; set; }

    [Column("deal_currency")]
    public short? DealCurrency { get; set; }

    [Column("arz_type_id")]
    public int? ArzTypeId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("gharardad_id")]
    public int? GharardadId { get; set; }

    [Column("karfarma_id")]
    public int? KarfarmaId { get; set; }

    [Column("karfarma_address_id")]
    public int? KarfarmaAddressId { get; set; }

    [Column("karfarma_phone_id")]
    public int? KarfarmaPhoneId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [Column("voucher_item_id")]
    public int VoucherItemId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TtmsEmployerInfos")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TtmsEmployerInfos")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("TtmsEmployerInfos")]
    public virtual Voucher Voucher { get; set; } = null!;

    [ForeignKey("VoucherItemId")]
    [InverseProperty("TtmsEmployerInfo")]
    public virtual VoucherItem VoucherItem { get; set; } = null!;
}
