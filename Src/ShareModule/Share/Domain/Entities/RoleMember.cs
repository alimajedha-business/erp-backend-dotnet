using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("role_members", Schema = "shared")]
[Index("CompanyId", Name = "role_members_company_id_c28413ce")]
[Index("MemberId", Name = "role_members_member_id_0a61f922")]
[Index("RoleId", Name = "role_members_role_id_3742ecb7")]
public partial class RoleMember
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RoleMembers")]
    public virtual Role Role { get; set; } = null!;
}
