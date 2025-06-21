using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("entity_type_dependencies", Schema = "general")]
[Index("EntityTypeId", Name = "entity_type_dependencies_entity_type_id_e631567c")]
[Index("RequiredEntityTypeId", Name = "entity_type_dependencies_required_entity_type_id_64bab813")]
public partial class EntityTypeDependency
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("read")]
    public bool Read { get; set; }

    [Column("create")]
    public bool Create { get; set; }

    [Column("edit")]
    public bool Edit { get; set; }

    [Column("delete")]
    public bool Delete { get; set; }

    [Column("log")]
    public bool Log { get; set; }

    [Column("print")]
    public bool Print { get; set; }

    [Column("imp")]
    public bool Imp { get; set; }

    [Column("exp")]
    public bool Exp { get; set; }

    [Column("if_not_creator")]
    public bool IfNotCreator { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("required_entity_type_id")]
    public int RequiredEntityTypeId { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("EntityTypeDependencyEntityTypes")]
    public virtual EntityType EntityType { get; set; } = null!;

    [ForeignKey("RequiredEntityTypeId")]
    [InverseProperty("EntityTypeDependencyRequiredEntityTypes")]
    public virtual EntityType RequiredEntityType { get; set; } = null!;
}
