using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("entity_type_constraints", Schema = "general")]
[Index("EntityTypeId", Name = "entity_type_constraints_entity_type_id_a7b4c730")]
public partial class EntityTypeConstraint
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("field_name")]
    [StringLength(100)]
    public string FieldName { get; set; } = null!;

    [Column("name_fa")]
    [StringLength(100)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(100)]
    public string NameEn { get; set; } = null!;

    [Column("readable")]
    public bool Readable { get; set; }

    [Column("creatable")]
    public bool Creatable { get; set; }

    [Column("editable")]
    public bool Editable { get; set; }

    [Column("printable")]
    public bool Printable { get; set; }

    [Column("importable")]
    public bool Importable { get; set; }

    [Column("exportable")]
    public bool Exportable { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("EntityTypeConstraints")]
    public virtual EntityType EntityType { get; set; } = null!;
}
