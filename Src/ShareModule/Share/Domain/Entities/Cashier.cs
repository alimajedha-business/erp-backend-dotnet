using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("cashiers", Schema = "shared")]
[Index("CompanyId", Name = "cashiers_company_id_e24f4c24")]
[Index("CreatorId", Name = "cashiers_creator_id_cd8bd2ff")]
[Index("PersonId", Name = "cashiers_person_id_2b0579d0")]
public partial class Cashier
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
