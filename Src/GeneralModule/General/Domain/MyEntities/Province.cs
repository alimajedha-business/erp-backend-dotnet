using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Domain.Entities
{
    [Table("provinces")]
    public class MyProvince
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        [Column("country_id")]
        public int CountryId { get; set; }
    }
}
