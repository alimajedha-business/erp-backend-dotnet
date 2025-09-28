using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Entities
{
    [Table("WarehouseType", Schema = "warehouse")]
    public partial class WarehouseType
    {
        [Key]
        public int Id { get; set; }

        public required int Code { get; set; } 

        [StringLength(50)]
        public required string Name { get; set; }

        public ICollection<WarehouseStock>? WarehouseStock { get; set; }

    }
}
