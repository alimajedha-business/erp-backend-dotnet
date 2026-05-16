using System.ComponentModel.DataAnnotations.Schema;
using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("roles", Schema = "shared")]
public class Role : BaseEntityWithCompany
{
    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("is_static")]
    public bool IsStatic { get; set; }

    [Column("authorized_users")]
    public string? AuthorizedUsers { get; set; }

    public virtual ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
}
