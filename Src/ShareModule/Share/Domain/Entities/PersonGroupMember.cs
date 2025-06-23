using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Entities;

[Table("person_group_members", Schema = "shared")]
[Index("CompanyId", Name = "person_group_members_company_id_730c5fff")]
[Index("MemberId", Name = "person_group_members_member_id_fb0c85d9")]
[Index("PersonGroupId", Name = "person_group_members_person_group_id_34698b20")]
public partial class PersonGroupMember
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("person_group_id")]
    public int PersonGroupId { get; set; }
}
