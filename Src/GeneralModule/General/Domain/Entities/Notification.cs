using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("notifications", Schema = "general")]
[Index("ModuleId", Name = "notifications_module_id_31f93f98")]
[Index("User2Id", Name = "notifications_user2_id_8fe256fe")]
[Index("UserId", Name = "notifications_user_id_468e288d")]
public partial class Notification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    [StringLength(14)]
    public string Type { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("count")]
    public int? Count { get; set; }

    [Column("display_count")]
    public bool DisplayCount { get; set; }

    [Column("read_at")]
    public DateTime? ReadAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("user2_id")]
    public int? User2Id { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("Notifications")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("NotificationUsers")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("User2Id")]
    [InverseProperty("NotificationUser2s")]
    public virtual User? User2 { get; set; }
}
