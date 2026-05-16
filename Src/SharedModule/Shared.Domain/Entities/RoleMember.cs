using System.ComponentModel.DataAnnotations.Schema;
using NGErp.Base.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("role_members", Schema = "shared")]
public class RoleMember : BaseEntity
{
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = default!;

    [Column("member_id")]
    public Guid MemberId { get; set; }

    [Column("authorized_users")]
    public string? AuthorizedUsers { get; set; }
}
