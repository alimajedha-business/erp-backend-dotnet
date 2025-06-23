using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("roles", Schema = "shared")]
[Index("CompanyId", Name = "roles_company_id_f4c539e9")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("is_static")]
    public bool IsStatic { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<RoleMember> RoleMembers { get; set; } = new List<RoleMember>();

    [InverseProperty("Role")]
    public virtual ICollection<RolePermissionCommand> RolePermissionCommands { get; set; } = new List<RolePermissionCommand>();

    [InverseProperty("Role")]
    public virtual ICollection<RolePermissionConstraint> RolePermissionConstraints { get; set; } = new List<RolePermissionConstraint>();

    [InverseProperty("Role")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
