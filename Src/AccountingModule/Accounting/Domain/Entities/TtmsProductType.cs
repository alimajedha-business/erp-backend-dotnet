using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ttms_product_types", Schema = "accounting")]
[Index("Code", Name = "UQ__ttms_pro__357D4CF9F1A81110", IsUnique = true)]
public partial class TtmsProductType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("code")]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [InverseProperty("KalaType")]
    public virtual ICollection<TtmsBuy> TtmsBuys { get; set; } = new List<TtmsBuy>();

    [InverseProperty("KalaType")]
    public virtual ICollection<TtmsExportation> TtmsExportations { get; set; } = new List<TtmsExportation>();

    [InverseProperty("KalaType")]
    public virtual ICollection<TtmsImportation> TtmsImportations { get; set; } = new List<TtmsImportation>();

    [InverseProperty("KalaType")]
    public virtual ICollection<TtmsSell> TtmsSells { get; set; } = new List<TtmsSell>();

    [InverseProperty("KalaType")]
    public virtual ICollection<TtmsWage> TtmsWages { get; set; } = new List<TtmsWage>();
}
