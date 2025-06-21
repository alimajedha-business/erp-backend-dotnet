using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("user_configs", Schema = "general")]
[Index("ModuleId", Name = "user_configs_module_id_015d8e4f")]
[Index("UserId", Name = "user_configs_user_id_c0e78255")]
public partial class UserConfig
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(4000)]
    public string Name { get; set; } = null!;

    [Column("data")]
    public string? Data { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("UserConfigs")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserConfigs")]
    public virtual User User { get; set; } = null!;
}
