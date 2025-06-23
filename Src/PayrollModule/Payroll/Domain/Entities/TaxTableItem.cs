using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("tax_table_items", Schema = "payroll")]
[Index("CreatorId", Name = "tax_table_items_creator_id_0497fa6d")]
[Index("TaxTableId", Name = "tax_table_items_tax_table_id_c6856ca1")]
public partial class TaxTableItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("from_amount", TypeName = "numeric(18, 2)")]
    public decimal FromAmount { get; set; }

    [Column("to_amount", TypeName = "numeric(18, 2)")]
    public decimal ToAmount { get; set; }

    [Column("tax_rate", TypeName = "numeric(5, 2)")]
    public decimal TaxRate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("tax_table_id")]
    public int TaxTableId { get; set; }

    [ForeignKey("TaxTableId")]
    [InverseProperty("TaxTableItems")]
    public virtual TaxTable TaxTable { get; set; } = null!;
}
