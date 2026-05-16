using System.ComponentModel.DataAnnotations.Schema;
using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("role_permission_constraints", Schema = "shared")]
public class RolePermissionConstraint : BaseEntity
{
    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = default!;

    [Column("entity_type_id")]
    public Guid EntityTypeId { get; set; }

    [ForeignKey(nameof(EntityTypeId))]
    public virtual EntityType EntityType { get; set; } = default!;

    [Column("role_permission_id")]
    public Guid RolePermissionId { get; set; }

    [ForeignKey(nameof(RolePermissionId))]
    public virtual RolePermission RolePermission { get; set; } = default!;

    [Column("field_name")]
    public string FieldName { get; set; } = default!;

    [Column("read")]
    public bool Read { get; set; }

    [Column("create")]
    public bool Create { get; set; }

    [Column("edit")]
    public bool Edit { get; set; }

    [Column("print")]
    public bool Print { get; set; }

    [Column("imp")]
    public bool Import { get; set; }

    [Column("exp")]
    public bool Export { get; set; }
}
