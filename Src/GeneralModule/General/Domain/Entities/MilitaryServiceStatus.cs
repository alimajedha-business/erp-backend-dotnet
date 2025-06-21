using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("military_service_statuses", Schema = "general")]
[Index("Name", Name = "UQ__military__72E12F1BB332E6F2", IsUnique = true)]
[Index("CreatorId", Name = "military_service_statuses_creator_id_2264fea4")]
public partial class MilitaryServiceStatus
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
    [InverseProperty("MilitaryServiceStatuses")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("MilitaryServiceStatus")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
