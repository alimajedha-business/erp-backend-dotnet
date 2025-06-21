using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ledger_period_companies", Schema = "accounting")]
[Index("CompanyId", Name = "ledger_period_companies_company_id_b4e49be1")]
[Index("LedgerId", Name = "ledger_period_companies_ledger_id_52a61c2a")]
[Index("LedgerPeriodId", Name = "ledger_period_companies_ledger_period_id_5656d93c")]
[Index("PeriodId", Name = "ledger_period_companies_period_id_0828d8ca")]
public partial class LedgerPeriodCompany
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("ledger_period_id")]
    public int LedgerPeriodId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("LedgerPeriodCompanies")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("LedgerPeriodId")]
    [InverseProperty("LedgerPeriodCompanies")]
    public virtual LedgerPeriod LedgerPeriod { get; set; } = null!;

    [InverseProperty("LedgerPeriodCompany")]
    public virtual LedgerPeriodCompanySetting? LedgerPeriodCompanySetting { get; set; }

    [ForeignKey("PeriodId")]
    [InverseProperty("LedgerPeriodCompanies")]
    public virtual Period Period { get; set; } = null!;
}
