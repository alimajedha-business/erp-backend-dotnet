using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_bank_accounts", Schema = "general")]
[Index("BankId", Name = "person_bank_accounts_bank_id_f4412c39")]
[Index("PersonId", Name = "person_bank_accounts_person_id_cea19acc")]
public partial class PersonBankAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("iban")]
    [StringLength(26)]
    public string? Iban { get; set; }

    [Column("account_number")]
    [StringLength(24)]
    public string? AccountNumber { get; set; }

    [Column("card_number")]
    [StringLength(24)]
    public string? CardNumber { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("bank_id")]
    public int BankId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("BankId")]
    [InverseProperty("PersonBankAccounts")]
    public virtual Bank Bank { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonBankAccounts")]
    public virtual Person Person { get; set; } = null!;
}
