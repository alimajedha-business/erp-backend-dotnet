using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_relatives", Schema = "general")]
[Index("BirthCityId", Name = "person_relatives_birth_city_id_6e4fe77e")]
[Index("CreatorId", Name = "person_relatives_creator_id_2e5156c1")]
[Index("PersonId", Name = "person_relatives_person_id_0ab58207")]
public partial class PersonRelative
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("family")]
    [StringLength(100)]
    public string? Family { get; set; }

    [Column("gender")]
    [StringLength(5)]
    public string? Gender { get; set; }

    [Column("father_name")]
    [StringLength(100)]
    public string? FatherName { get; set; }

    [Column("birth_date")]
    public DateOnly? BirthDate { get; set; }

    [Column("citizen_code")]
    [StringLength(10)]
    public string? CitizenCode { get; set; }

    [Column("id_number")]
    [StringLength(10)]
    public string? IdNumber { get; set; }

    [Column("relationship_type")]
    [StringLength(32)]
    public string RelationshipType { get; set; } = null!;

    [Column("under_guardianship")]
    public bool UnderGuardianship { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("birth_city_id")]
    public int? BirthCityId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("BirthCityId")]
    [InverseProperty("PersonRelatives")]
    public virtual City? BirthCity { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("PersonRelatives")]
    public virtual User Creator { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonRelatives")]
    public virtual Person Person { get; set; } = null!;
}
