using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("banks", Schema = "general")]
[Index("Name", Name = "UQ__banks__72E12F1BD4395708", IsUnique = true)]
public partial class Bank
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Bank")]
    public virtual BankStatementPattern? BankStatementPattern { get; set; }

    [InverseProperty("Bank")]
    public virtual ICollection<PersonBankAccount> PersonBankAccounts { get; set; } = new List<PersonBankAccount>();
}
