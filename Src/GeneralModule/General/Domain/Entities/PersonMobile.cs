using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_mobiles", Schema = "general")]
[Index("PersonId", Name = "person_mobiles_person_id_ab811f2d")]
public partial class PersonMobile
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("mobile")]
    [StringLength(11)]
    public string Mobile { get; set; } = null!;

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("PersonMobiles")]
    public virtual Person Person { get; set; } = null!;
}
