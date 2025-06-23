using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("role_permission_constraints", Schema = "shared")]
[Index("EntityTypeId", Name = "role_permission_constraints_entity_type_id_183e495f")]
[Index("RoleId", Name = "role_permission_constraints_role_id_3ad70fc8")]
[Index("RolePermissionId", Name = "role_permission_constraints_role_permission_id_678eb8f6")]
public partial class RolePermissionConstraint
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("field_name")]
    [StringLength(100)]
    public string FieldName { get; set; } = null!;

    [Column("read")]
    public bool Read { get; set; }

    [Column("create")]
    public bool Create { get; set; }

    [Column("edit")]
    public bool Edit { get; set; }

    [Column("print")]
    public bool Print { get; set; }

    [Column("imp")]
    public bool Imp { get; set; }

    [Column("exp")]
    public bool Exp { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("role_permission_id")]
    public int RolePermissionId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissionConstraints")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("RolePermissionId")]
    [InverseProperty("RolePermissionConstraints")]
    public virtual RolePermission RolePermission { get; set; } = null!;
}
