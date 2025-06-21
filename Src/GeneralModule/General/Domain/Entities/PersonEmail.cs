using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_emails", Schema = "general")]
[Index("PersonId", Name = "person_emails_person_id_7ab1a818")]
public partial class PersonEmail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("email")]
    [StringLength(254)]
    public string Email { get; set; } = null!;

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("PersonEmails")]
    public virtual Person Person { get; set; } = null!;
}
