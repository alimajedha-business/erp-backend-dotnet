using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("products", Schema = "shared")]
[Index("CompanyId", Name = "products_company_id_85cab11e")]
[Index("CreatorId", Name = "products_creator_id_f217ad7d")]
[Index("FirstMeasurementId", Name = "products_first_measurement_id_c9eb56e6")]
[Index("ForthMeasurementId", Name = "products_forth_measurement_id_2571e159")]
[Index("ParentId", Name = "products_parent_id_6d11d0e1")]
[Index("SecondMeasurementId", Name = "products_second_measurement_id_9e456e28")]
[Index("ThirdMeasurementId", Name = "products_third_measurement_id_40c75e99")]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("level")]
    public short? Level { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("first_measurement_id")]
    public int? FirstMeasurementId { get; set; }

    [Column("forth_measurement_id")]
    public int? ForthMeasurementId { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [Column("second_measurement_id")]
    public int? SecondMeasurementId { get; set; }

    [Column("third_measurement_id")]
    public int? ThirdMeasurementId { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Foo> Foos { get; set; } = new List<Foo>();

    [InverseProperty("Parent")]
    public virtual ICollection<Product> InverseParent { get; set; } = new List<Product>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual Product? Parent { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("Product")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
