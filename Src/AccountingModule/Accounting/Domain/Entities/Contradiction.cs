using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("contradictions", Schema = "accounting")]
[Index("BankAccountId", Name = "contradictions_bank_account_id_e4432ed1")]
[Index("CompanyId", Name = "contradictions_company_id_f68c3f68")]
[Index("CreatorId", Name = "contradictions_creator_id_430d02a0")]
[Index("DataImportId", Name = "contradictions_data_import_id_65a4748b")]
[Index("LedgerId", Name = "contradictions_ledger_id_85776d2a")]
[Index("PeriodId", Name = "contradictions_period_id_538ee815")]
public partial class Contradiction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("bank_account_id")]
    public int BankAccountId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("data_import_id")]
    public int DataImportId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [InverseProperty("Contradiction")]
    public virtual ICollection<ContradictionItem> ContradictionItems { get; set; } = new List<ContradictionItem>();

    [ForeignKey("DataImportId")]
    [InverseProperty("Contradictions")]
    public virtual DataImport DataImport { get; set; } = null!;

    [ForeignKey("LedgerId")]
    [InverseProperty("Contradictions")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("Contradictions")]
    public virtual Period Period { get; set; } = null!;
}
