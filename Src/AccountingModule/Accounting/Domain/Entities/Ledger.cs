using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Domain.Entities;

[Table("Ledgers")]
public class Ledger
{
    [Key]
    public int Id { get; set; }
    [Required]
    public short Code { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name2 { get; set; }
    [Required]
    [Column("is_leading")]
    public bool IsLeading { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    [Required]
    [Column("created_at", TypeName = "datetime2(7)")]
    public DateTime CreatedAt { get; set; }
}