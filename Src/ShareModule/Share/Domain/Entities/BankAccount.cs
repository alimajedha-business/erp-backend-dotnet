using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("bank_accounts", Schema = "shared")]
[Index("BankBranchId", Name = "bank_accounts_bank_branch_id_f4462cb3")]
[Index("BankId", Name = "bank_accounts_bank_id_76519d0e")]
[Index("CompanyId", Name = "bank_accounts_company_id_a0fdfe1f")]
[Index("CurrencyId", Name = "bank_accounts_currency_id_932d5611")]
[Index("TypeId", Name = "bank_accounts_type_id_50bc1431")]
public partial class BankAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("iban")]
    [StringLength(26)]
    public string? Iban { get; set; }

    [Column("account_number")]
    [StringLength(24)]
    public string? AccountNumber { get; set; }

    [Column("card_number")]
    [StringLength(16)]
    public string? CardNumber { get; set; }

    [Column("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    [Column("account_owner")]
    [StringLength(150)]
    public string? AccountOwner { get; set; }

    [Column("account_type")]
    [StringLength(10)]
    public string? AccountType { get; set; }

    [Column("account_opening_date")]
    public DateOnly? AccountOpeningDate { get; set; }

    [Column("signature_expiry_date")]
    public DateOnly? SignatureExpiryDate { get; set; }

    [Column("national_code")]
    [StringLength(11)]
    public string? NationalCode { get; set; }

    [Column("signatory_1")]
    [StringLength(150)]
    public string? Signatory1 { get; set; }

    [Column("signatory_2")]
    [StringLength(150)]
    public string? Signatory2 { get; set; }

    [Column("signatory_3")]
    [StringLength(150)]
    public string? Signatory3 { get; set; }

    [Column("signatory_4")]
    [StringLength(150)]
    public string? Signatory4 { get; set; }

    [Column("signatory_5")]
    [StringLength(150)]
    public string? Signatory5 { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("bank_id")]
    public int BankId { get; set; }

    [Column("bank_branch_id")]
    public int BankBranchId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("currency_id")]
    public int? CurrencyId { get; set; }

    [Column("type_id")]
    public int TypeId { get; set; }

    [ForeignKey("BankBranchId")]
    [InverseProperty("BankAccounts")]
    public virtual BankBranch BankBranch { get; set; } = null!;
}
