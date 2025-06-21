using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("petty_cashiers", Schema = "shared")]
[Index("CompanyId", Name = "petty_cashiers_company_id_adaef187")]
[Index("CreatorId", Name = "petty_cashiers_creator_id_d570b446")]
[Index("PersonId", Name = "petty_cashiers_person_id_5a0c025f")]
public partial class PettyCashier
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }
}
