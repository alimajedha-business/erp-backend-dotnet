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
    [Table("VehicleType", Schema = "shared")]
 
    public partial class VehicleType
    {
        [Key]        
        public int Id { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; } = null!;
        
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        public int CompanyId { get; set; }

    }
}
