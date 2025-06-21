using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("bank_statement_patterns", Schema = "general")]
[Index("BankId", Name = "UQ__bank_sta__4076F702CA9D216F", IsUnique = true)]
public partial class BankStatementPattern
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("column_cheque_no")]
    [StringLength(1)]
    public string ColumnChequeNo { get; set; } = null!;

    [Column("column_cheque_date")]
    [StringLength(1)]
    public string ColumnChequeDate { get; set; } = null!;

    [Column("column_debit")]
    [StringLength(1)]
    public string ColumnDebit { get; set; } = null!;

    [Column("column_credit")]
    [StringLength(1)]
    public string ColumnCredit { get; set; } = null!;

    [Column("first_row_no")]
    public short FirstRowNo { get; set; }

    [Column("ignore_latest_rows")]
    public short IgnoreLatestRows { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("bank_id")]
    public int BankId { get; set; }

    [ForeignKey("BankId")]
    [InverseProperty("BankStatementPattern")]
    public virtual Bank Bank { get; set; } = null!;
}
