using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("account_set_items", Schema = "accounting")]
[Index("AccountSetId", Name = "account_set_items_account_set_id_c6a49338")]
[Index("MasterId", Name = "account_set_items_master_id_e12671e4")]
[Index("SlaveId", Name = "account_set_items_slave_id_a5287d0f")]
public partial class AccountSetItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("account_set_id")]
    public int AccountSetId { get; set; }

    [Column("master_id")]
    public int? MasterId { get; set; }

    [Column("slave_id")]
    public int? SlaveId { get; set; }

    [ForeignKey("AccountSetId")]
    [InverseProperty("AccountSetItems")]
    public virtual AccountSet AccountSet { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("AccountSetItems")]
    public virtual MasterAccount? Master { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("AccountSetItems")]
    public virtual SlaveAccount? Slave { get; set; }
}
