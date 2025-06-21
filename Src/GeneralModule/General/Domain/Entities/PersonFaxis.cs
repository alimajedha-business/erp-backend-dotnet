using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_faxes", Schema = "general")]
[Index("PersonId", Name = "person_faxes_person_id_aeabbb62")]
public partial class PersonFaxis
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("code")]
    [StringLength(8)]
    public string? Code { get; set; }

    [Column("fax")]
    [StringLength(12)]
    public string Fax { get; set; } = null!;

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("PersonFaxes")]
    public virtual Person Person { get; set; } = null!;
}
