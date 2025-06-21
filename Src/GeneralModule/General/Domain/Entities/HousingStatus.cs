using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("housing_statuses", Schema = "general")]
[Index("Name", Name = "UQ__housing___72E12F1BF71088F1", IsUnique = true)]
[Index("CreatorId", Name = "housing_statuses_creator_id_b7300b34")]
public partial class HousingStatus
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
    [InverseProperty("HousingStatuses")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("HousingStatus")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
