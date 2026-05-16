using System.ComponentModel.DataAnnotations.Schema;
using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("role_permissions", Schema = "shared")]
public class RolePermission : BaseEntity
{
    [Column("module_id")]
    public long ModuleId { get; set; }

    [ForeignKey(nameof(ModuleId))]
    public virtual Module Module { get; set; } = default!;

    [Column("entity_type_id")]
    public Guid EntityTypeId { get; set; }

    [ForeignKey(nameof(EntityTypeId))]
    public virtual EntityType EntityType { get; set; } = default!;

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
    public bool Import { get; set; }

    [Column("exp")]
    public bool Export { get; set; }

    [Column("if_not_creator")]
    public bool IfNotCreator { get; set; }

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = default!;

    public virtual ICollection<RolePermissionCommand> Commands { get; set; } = new List<RolePermissionCommand>();
    public virtual ICollection<RolePermissionConstraint> Constraints { get; set; } = new List<RolePermissionConstraint>();
}
