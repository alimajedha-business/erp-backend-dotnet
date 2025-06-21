using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("entity_type_commands", Schema = "general")]
[Index("EntityTypeId", Name = "entity_type_commands_entity_type_id_c94a0ef5")]
public partial class EntityTypeCommand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("key")]
    [StringLength(100)]
    public string Key { get; set; } = null!;

    [Column("name_fa")]
    [StringLength(100)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(100)]
    public string NameEn { get; set; } = null!;

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("EntityTypeCommands")]
    public virtual EntityType EntityType { get; set; } = null!;

    [InverseProperty("EntityTypeCommand")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
