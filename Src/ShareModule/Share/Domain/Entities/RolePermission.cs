using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("role_permissions", Schema = "shared")]
[Index("EntityTypeId", Name = "role_permissions_entity_type_id_74eb6fa5")]
[Index("ModuleId", Name = "role_permissions_module_id_f6701517")]
[Index("RoleId", Name = "role_permissions_role_id_216516f2")]
public partial class RolePermission
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

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissions")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("RolePermission")]
    public virtual ICollection<RolePermissionCommand> RolePermissionCommands { get; set; } = new List<RolePermissionCommand>();

    [InverseProperty("RolePermission")]
    public virtual ICollection<RolePermissionConstraint> RolePermissionConstraints { get; set; } = new List<RolePermissionConstraint>();
}
