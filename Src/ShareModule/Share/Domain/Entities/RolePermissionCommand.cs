using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("role_permission_commands", Schema = "shared")]
[Index("EntityTypeCommandId", Name = "role_permission_commands_entity_type_command_id_9e625b38")]
[Index("EntityTypeId", Name = "role_permission_commands_entity_type_id_a3513f52")]
[Index("RoleId", Name = "role_permission_commands_role_id_5d1758cb")]
[Index("RolePermissionId", Name = "role_permission_commands_role_permission_id_40a8626f")]
public partial class RolePermissionCommand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("command_key")]
    [StringLength(100)]
    public string CommandKey { get; set; } = null!;

    [Column("value")]
    public bool Value { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("entity_type_command_id")]
    public int EntityTypeCommandId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("role_permission_id")]
    public int RolePermissionId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissionCommands")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("RolePermissionId")]
    [InverseProperty("RolePermissionCommands")]
    public virtual RolePermission RolePermission { get; set; } = null!;
}
