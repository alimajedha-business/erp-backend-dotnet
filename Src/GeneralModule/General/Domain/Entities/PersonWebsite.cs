using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_websites", Schema = "general")]
[Index("PersonId", Name = "person_websites_person_id_2bf2e8a6")]
public partial class PersonWebsite
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("website")]
    [StringLength(200)]
    public string Website { get; set; } = null!;

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("PersonWebsites")]
    public virtual Person Person { get; set; } = null!;
}
