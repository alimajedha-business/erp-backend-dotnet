using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ledger_periods", Schema = "accounting")]
[Index("LedgerId", Name = "ledger_periods_ledger_id_c70ff025")]
[Index("PeriodId", Name = "ledger_periods_period_id_2953e5ba")]
public partial class LedgerPeriod
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("LedgerPeriods")]
    public virtual Ledger Ledger { get; set; } = null!;

    [InverseProperty("LedgerPeriod")]
    public virtual ICollection<LedgerPeriodCompany> LedgerPeriodCompanies { get; set; } = new List<LedgerPeriodCompany>();

    [ForeignKey("PeriodId")]
    [InverseProperty("LedgerPeriods")]
    public virtual Period Period { get; set; } = null!;
}
