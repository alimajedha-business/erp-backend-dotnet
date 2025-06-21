using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("entity_types", Schema = "general")]
[Index("ContentTypeId", Name = "entity_types_content_type_id_b7d4130b")]
[Index("ModuleId", Name = "entity_types_module_id_55d1da29")]
public partial class EntityType
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

    [Column("readable")]
    public bool Readable { get; set; }

    [Column("creatable")]
    public bool Creatable { get; set; }

    [Column("editable")]
    public bool Editable { get; set; }

    [Column("deletable")]
    public bool Deletable { get; set; }

    [Column("loggable")]
    public bool Loggable { get; set; }

    [Column("printable")]
    public bool Printable { get; set; }

    [Column("importable")]
    public bool Importable { get; set; }

    [Column("exportable")]
    public bool Exportable { get; set; }

    [Column("if_not_creator")]
    public bool IfNotCreator { get; set; }

    [Column("has_restriction")]
    public bool HasRestriction { get; set; }

    [Column("permissible")]
    public bool Permissible { get; set; }

    [Column("has_constraint")]
    public bool HasConstraint { get; set; }

    [Column("content_type_id")]
    public int? ContentTypeId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [InverseProperty("EntityType")]
    public virtual ICollection<EntityTypeCommand> EntityTypeCommands { get; set; } = new List<EntityTypeCommand>();

    [InverseProperty("EntityType")]
    public virtual ICollection<EntityTypeConstraint> EntityTypeConstraints { get; set; } = new List<EntityTypeConstraint>();

    [InverseProperty("EntityType")]
    public virtual ICollection<EntityTypeDependency> EntityTypeDependencyEntityTypes { get; set; } = new List<EntityTypeDependency>();

    [InverseProperty("RequiredEntityType")]
    public virtual ICollection<EntityTypeDependency> EntityTypeDependencyRequiredEntityTypes { get; set; } = new List<EntityTypeDependency>();

    [InverseProperty("EntityType")]
    public virtual ICollection<FileEntity> FileEntities { get; set; } = new List<FileEntity>();

    [InverseProperty("EntityType")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("EntityType")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    [ForeignKey("ModuleId")]
    [InverseProperty("EntityTypes")]
    public virtual Module Module { get; set; } = null!;

    [InverseProperty("EntityType")]
    public virtual ICollection<SelectLog> SelectLogs { get; set; } = new List<SelectLog>();
}
