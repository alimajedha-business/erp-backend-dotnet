using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("slave_account_standard_descriptions", Schema = "accounting")]
[Index("CompanyId", Name = "slave_account_standard_descriptions_company_id_a2d12c8c")]
[Index("SlaveId", Name = "slave_account_standard_descriptions_slave_id_b64d916e")]
public partial class SlaveAccountStandardDescription
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("slave_id")]
    public int SlaveId { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("SlaveAccountStandardDescriptions")]
    public virtual SlaveAccount Slave { get; set; } = null!;
}
