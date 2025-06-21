using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("settings", Schema = "accounting")]
[Index("DomainId", Name = "UQ__settings__E72BC7677365AE8A", IsUnique = true)]
public partial class Setting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("s1")]
    public bool S1 { get; set; }

    [Column("domain_id")]
    public int DomainId { get; set; }
}
