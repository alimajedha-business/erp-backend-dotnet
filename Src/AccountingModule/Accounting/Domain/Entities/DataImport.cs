using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("data_imports", Schema = "accounting")]
[Index("CompanyId", Name = "data_imports_company_id_eda746f3")]
[Index("CreatorId", Name = "data_imports_creator_id_cba47975")]
[Index("EntityTypeCommandId", Name = "data_imports_entity_type_command_id_a8a0c6e3")]
[Index("EntityTypeId", Name = "data_imports_entity_type_id_904ccc0a")]
[Index("LedgerId", Name = "data_imports_ledger_id_fd1c5e0d")]
[Index("PeriodId", Name = "data_imports_period_id_aef725fd")]
public partial class DataImport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("related_file")]
    [StringLength(100)]
    public string RelatedFile { get; set; } = null!;

    [Column("error_file")]
    [StringLength(100)]
    public string? ErrorFile { get; set; }

    [Column("file_type")]
    [StringLength(15)]
    public string FileType { get; set; } = null!;

    [Column("status")]
    [StringLength(18)]
    public string Status { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("entity_type_command_id")]
    public int EntityTypeCommandId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [InverseProperty("DataImport")]
    public virtual ICollection<Contradiction> Contradictions { get; set; } = new List<Contradiction>();

    [ForeignKey("LedgerId")]
    [InverseProperty("DataImports")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("DataImports")]
    public virtual Period Period { get; set; } = null!;
}
