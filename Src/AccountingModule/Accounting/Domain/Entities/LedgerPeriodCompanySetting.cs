using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ledger_period_company_settings", Schema = "accounting")]
[Index("LedgerPeriodCompanyId", Name = "UQ__ledger_p__8880943C4AE414BF", IsUnique = true)]
[Index("CompanyId", Name = "ledger_period_company_settings_company_id_a0d2dc4a")]
[Index("LedgerId", Name = "ledger_period_company_settings_ledger_id_c37251eb")]
[Index("PeriodId", Name = "ledger_period_company_settings_period_id_2389d1c0")]
public partial class LedgerPeriodCompanySetting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("is_closed")]
    public bool IsClosed { get; set; }

    [Column("s1")]
    public bool S1 { get; set; }

    [Column("s2")]
    public bool S2 { get; set; }

    [Column("s3")]
    public bool S3 { get; set; }

    [Column("s4")]
    public bool S4 { get; set; }

    [Column("s5")]
    public bool S5 { get; set; }

    [Column("s6")]
    public bool S6 { get; set; }

    [Column("s7")]
    public bool S7 { get; set; }

    [Column("s8")]
    public bool S8 { get; set; }

    [Column("s9")]
    public bool S9 { get; set; }

    [Column("s10")]
    public bool S10 { get; set; }

    [Column("s11")]
    public bool S11 { get; set; }

    [Column("s12")]
    public bool S12 { get; set; }

    [Column("s13")]
    public bool S13 { get; set; }

    [Column("s14")]
    public short? S14 { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("ledger_period_company_id")]
    public int LedgerPeriodCompanyId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("LedgerPeriodCompanySettings")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("LedgerPeriodCompanyId")]
    [InverseProperty("LedgerPeriodCompanySetting")]
    public virtual LedgerPeriodCompany LedgerPeriodCompany { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("LedgerPeriodCompanySettings")]
    public virtual Period Period { get; set; } = null!;
}
