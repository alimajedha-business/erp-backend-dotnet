using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Entities
{
    [Table("vehicle_types", Schema = "shared")]
 
    public partial class VehicleType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [StringLength(10)]
        public string Code { get; set; } = null!;

        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Column("company_id")]
        public int CompanyId { get; set; }

    }
}
