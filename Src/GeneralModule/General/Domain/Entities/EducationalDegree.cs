using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("educational_degrees", Schema = "general")]
[Index("Name", Name = "UQ__educatio__72E12F1B3436D9CA", IsUnique = true)]
[Index("CreatorId", Name = "educational_degrees_creator_id_693f4fac")]
public partial class EducationalDegree
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("tax_educational_degree")]
    [StringLength(20)]
    public string TaxEducationalDegree { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("EducationalDegrees")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("EducationalDegree")]
    public virtual ICollection<PersonEducationalDegree> PersonEducationalDegrees { get; set; } = new List<PersonEducationalDegree>();
}
