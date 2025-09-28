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
    [Table("WarehouseTypes", Schema = "Warehouse")]
    public partial class WarehouseType
    {
        [Key]
        public int Id { get; set; }

        public int TypeCode { get; set; } 

        [StringLength(50)]
        public string TypeName { get; set; } = String.Empty;

        //public ICollection<WarehouseStock> WarehouseStocks { get; set; }= new HashSet<WarehouseStock>();

    }
}
