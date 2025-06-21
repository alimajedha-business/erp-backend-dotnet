using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("bank_operation_types", Schema = "general")]
[Index("Code", Name = "UQ__bank_ope__357D4CF9707FC79A", IsUnique = true)]
[Index("Name", Name = "UQ__bank_ope__72E12F1B40012EEB", IsUnique = true)]
public partial class BankOperationType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
