using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("error_logs", Schema = "general")]
[Index("UserId", Name = "error_logs_user_id_ac744206")]
public partial class ErrorLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [Column("path")]
    [StringLength(255)]
    public string Path { get; set; } = null!;

    [Column("method")]
    [StringLength(10)]
    public string Method { get; set; } = null!;

    [Column("status_code")]
    public int StatusCode { get; set; }

    [Column("error_message")]
    public string ErrorMessage { get; set; } = null!;

    [Column("traceback")]
    public string? Traceback { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("code")]
    public string? Code { get; set; }

    [Column("extra")]
    public string? Extra { get; set; }

    [Column("curl_command")]
    public string? CurlCommand { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ErrorLogs")]
    public virtual User? User { get; set; }
}
