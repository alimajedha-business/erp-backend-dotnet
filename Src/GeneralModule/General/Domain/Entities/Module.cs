using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("modules", Schema = "general")]
[Index("NameEn", Name = "UQ__modules__2E0A72B0D7D1DAD7", IsUnique = true)]
[Index("NameFa", Name = "UQ__modules__2E0A7AA66BFF07F4", IsUnique = true)]
public partial class Module
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name_fa")]
    [StringLength(50)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(50)]
    public string NameEn { get; set; } = null!;

    [Column("color")]
    [StringLength(6)]
    public string Color { get; set; } = null!;

    [Column("prefix")]
    [StringLength(40)]
    public string Prefix { get; set; } = null!;

    [InverseProperty("Module")]
    public virtual ICollection<CompanyModule> CompanyModules { get; set; } = new List<CompanyModule>();

    [InverseProperty("Module")]
    public virtual ICollection<EntityType> EntityTypes { get; set; } = new List<EntityType>();

    [InverseProperty("Module")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("Module")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    [InverseProperty("Module")]
    public virtual ICollection<ModulePersonGroup> ModulePersonGroups { get; set; } = new List<ModulePersonGroup>();

    [InverseProperty("Module")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("Module")]
    public virtual ICollection<SelectLog> SelectLogs { get; set; } = new List<SelectLog>();

    [InverseProperty("Module")]
    public virtual ICollection<UserConfig> UserConfigs { get; set; } = new List<UserConfig>();
}
