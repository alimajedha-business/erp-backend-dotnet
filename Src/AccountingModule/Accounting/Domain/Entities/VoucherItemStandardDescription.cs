using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("voucher_item_standard_descriptions", Schema = "accounting")]
[Index("CompanyId", Name = "voucher_item_standard_descriptions_company_id_c35a06a8")]
public partial class VoucherItemStandardDescription
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Column("company_id")]
    public int CompanyId { get; set; }
}
