using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("module_person_groups", Schema = "general")]
[Index("ModuleId", Name = "module_person_groups_module_id_64506255")]
[Index("PersonGroupId", Name = "module_person_groups_person_group_id_1b684782")]
public partial class ModulePersonGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("person_group_id")]
    public int PersonGroupId { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("ModulePersonGroups")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("PersonGroupId")]
    [InverseProperty("ModulePersonGroups")]
    public virtual PersonGroup PersonGroup { get; set; } = null!;
}
