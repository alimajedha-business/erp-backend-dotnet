using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("restrictions", Schema = "shared")]
[Index("CompanyId", Name = "restrictions_company_id_e24e8bce")]
[Index("EntityTypeId", Name = "restrictions_entity_type_id_5861bb50")]
[Index("UserId", Name = "restrictions_user_id_1ef45563")]
public partial class Restriction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
}
