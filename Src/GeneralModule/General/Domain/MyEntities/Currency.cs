using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Domain.Entities
{
    [Table("currencies")]
    public class MyCurrency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        public int Code { get; set; }
        [MaxLength(10)]
        public string? Symbol { get; set; }
        [MaxLength(3)]
        public required string Iso { get; set; }
        [Required]
        [Column("created_at", TypeName = "datetime2(7)")]
        public DateTime CreatedAt { get; set; }
    }
}
