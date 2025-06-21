using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("person_educational_degrees", Schema = "general")]
[Index("CreatorId", Name = "person_educational_degrees_creator_id_29916c83")]
[Index("EducationalDegreeId", Name = "person_educational_degrees_educational_degree_id_59f16fbf")]
[Index("PersonId", Name = "person_educational_degrees_person_id_c8759020")]
public partial class PersonEducationalDegree
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("study_field")]
    [StringLength(100)]
    public string? StudyField { get; set; }

    [Column("educational_institution")]
    [StringLength(100)]
    public string? EducationalInstitution { get; set; }

    [Column("start_date")]
    public DateOnly? StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("gpa", TypeName = "numeric(5, 2)")]
    public decimal? Gpa { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("educational_degree_id")]
    public int EducationalDegreeId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("PersonEducationalDegrees")]
    public virtual User Creator { get; set; } = null!;

    [ForeignKey("EducationalDegreeId")]
    [InverseProperty("PersonEducationalDegrees")]
    public virtual EducationalDegree EducationalDegree { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonEducationalDegrees")]
    public virtual Person Person { get; set; } = null!;
}
