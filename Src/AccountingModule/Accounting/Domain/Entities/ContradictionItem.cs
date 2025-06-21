using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("contradiction_items", Schema = "accounting")]
[Index("ContradictionId", Name = "contradiction_items_contradiction_id_1494f375")]
[Index("CreatorId", Name = "contradiction_items_creator_id_ee0be02e")]
public partial class ContradictionItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("doc_date")]
    public DateOnly? DocDate { get; set; }

    [Column("doc_no")]
    [StringLength(30)]
    public string? DocNo { get; set; }

    [Column("debit")]
    public long? Debit { get; set; }

    [Column("credit")]
    public long? Credit { get; set; }

    [Column("is_tick")]
    public bool IsTick { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("contradiction_id")]
    public int ContradictionId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("ContradictionId")]
    [InverseProperty("ContradictionItems")]
    public virtual Contradiction Contradiction { get; set; } = null!;
}
