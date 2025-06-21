using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("sales", Schema = "shared")]
[Index("CompanyId", Name = "sales_company_id_1480331f")]
[Index("ProductId", Name = "sales_product_id_a179a813")]
public partial class Sale
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
    [InverseProperty("Sales")]
    public virtual Product Product { get; set; } = null!;
}
