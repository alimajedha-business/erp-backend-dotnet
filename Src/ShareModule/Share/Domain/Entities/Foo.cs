using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("foos", Schema = "shared")]
[Index("CompanyId", Name = "foos_company_id_1a5e3040")]
[Index("ProductId", Name = "foos_product_id_64577d3d")]
public partial class Foo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Column("amount", TypeName = "numeric(10, 2)")]
    public decimal Amount { get; set; }

    [Column("count")]
    public int Count { get; set; }

    [Column("text")]
    public string? Text { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Foos")]
    public virtual Product Product { get; set; } = null!;
}
