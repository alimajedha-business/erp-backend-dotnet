using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("Purchases", Schema = "shared")]
[Index("CompanyId", Name = "Purchases_company_id_a3e8b6cd")]
[Index("ProductId", Name = "Purchases_product_id_d1413dc0")]
public partial class Purchase
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
    [InverseProperty("Purchases")]
    public virtual Product Product { get; set; } = null!;
}
