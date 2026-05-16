using System.ComponentModel.DataAnnotations.Schema;
using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("role_permission_commands", Schema = "shared")]
public class RolePermissionCommand : BaseEntity
{
    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = default!;

    [Column("entity_type_id")]
    public Guid EntityTypeId { get; set; }

    [ForeignKey(nameof(EntityTypeId))]
    public virtual EntityType EntityType { get; set; } = default!;

    [Column("command_key")]
    public string CommandKey { get; set; } = default!;

    [Column("role_permission_id")]
    public Guid RolePermissionId { get; set; }

    [ForeignKey(nameof(RolePermissionId))]
    public virtual RolePermission RolePermission { get; set; } = default!;

    [Column("entity_type_command_id")]
    public Guid EntityTypeCommandId { get; set; }

    [ForeignKey(nameof(EntityTypeCommandId))]
    public virtual EntityTypeCommand EntityTypeCommand { get; set; } = default!;

    [Column("value")]
    public bool Value { get; set; }
}
