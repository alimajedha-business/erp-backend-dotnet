using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("account_categories", Schema = "accounting")]
[Index("Name", Name = "UQ__account___72E12F1B3B565389", IsUnique = true)]
public partial class AccountCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<AccountGroup> AccountGroups { get; set; } = new List<AccountGroup>();

    [InverseProperty("Category")]
    public virtual ICollection<SlaveAccount> SlaveAccounts { get; set; } = new List<SlaveAccount>();
}
