using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("foreign_languages", Schema = "general")]
[Index("Name", Name = "UQ__foreign___72E12F1B6F35B462", IsUnique = true)]
[Index("CreatorId", Name = "foreign_languages_creator_id_f2c0414f")]
public partial class ForeignLanguage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ForeignLanguages")]
    public virtual User Creator { get; set; } = null!;
}
