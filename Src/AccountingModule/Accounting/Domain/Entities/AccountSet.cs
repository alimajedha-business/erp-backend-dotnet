using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("account_sets", Schema = "accounting")]
[Index("CompanyId", Name = "account_sets_company_id_227009e4")]
[Index("LedgerId", Name = "account_sets_ledger_id_31846310")]
public partial class AccountSet
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [InverseProperty("AccountSet")]
    public virtual ICollection<AccountSetItem> AccountSetItems { get; set; } = new List<AccountSetItem>();

    [ForeignKey("LedgerId")]
    [InverseProperty("AccountSets")]
    public virtual Ledger Ledger { get; set; } = null!;
}
