using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("domains", Schema = "general")]
[Index("Hostname", Name = "UQ__domains__DA92E433B526F86E", IsUnique = true)]
[Index("LanguageId", Name = "domains_language_id_a7140b25")]
public partial class Domain
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("hostname")]
    [StringLength(50)]
    public string Hostname { get; set; } = null!;

    [Column("holding")]
    [StringLength(100)]
    public string? Holding { get; set; }

    [Column("password_level")]
    [StringLength(7)]
    public string PasswordLevel { get; set; } = null!;

    [Column("auth_method")]
    [StringLength(50)]
    public string AuthMethod { get; set; } = null!;

    [Column("auth_send_type")]
    [StringLength(12)]
    public string? AuthSendType { get; set; }

    [Column("api_key")]
    [StringLength(100)]
    public string ApiKey { get; set; } = null!;

    [Column("template_name")]
    [StringLength(100)]
    public string TemplateName { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [InverseProperty("Domain")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [ForeignKey("LanguageId")]
    [InverseProperty("Domains")]
    public virtual Language Language { get; set; } = null!;
}
