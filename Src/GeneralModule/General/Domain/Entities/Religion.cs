using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("religions", Schema = "general")]
[Index("Name", Name = "UQ__religion__72E12F1B16AF9102", IsUnique = true)]
[Index("CreatorId", Name = "religions_creator_id_65bb4aaf")]
public partial class Religion
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
    [InverseProperty("Religions")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("Religion")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
