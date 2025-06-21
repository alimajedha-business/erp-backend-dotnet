using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("company_admins", Schema = "general")]
[Index("AdminId", Name = "company_admins_admin_id_7fe4b0ca")]
[Index("CompanyId", Name = "company_admins_company_id_dc4e1bcb")]
[Index("CreatorId", Name = "company_admins_creator_id_af4a192b")]
public partial class CompanyAdmin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("CompanyAdminAdmins")]
    public virtual User Admin { get; set; } = null!;

    [ForeignKey("CompanyId")]
    [InverseProperty("CompanyAdmins")]
    public virtual Company? Company { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("CompanyAdminCreators")]
    public virtual User Creator { get; set; } = null!;
}
