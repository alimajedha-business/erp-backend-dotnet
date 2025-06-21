using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("account_groups", Schema = "accounting")]
[Index("CategoryId", Name = "account_groups_category_id_af0c53e8")]
public partial class AccountGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("code")]
    public short Code { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("AccountGroups")]
    public virtual AccountCategory Category { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<SlaveAccount> SlaveAccounts { get; set; } = new List<SlaveAccount>();
}
