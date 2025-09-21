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
    [Table("ProductHierarchies", Schema = "Warehouse")]
    public class ProductHierarchy
    {
        [Key]
        public int Id { get; set; }

        public byte FirstLevelSize { get; set; } = 0!;

        [StringLength(10)]
        public string FirstLevelType { get; set; } = null!;


        public byte SecondLevelSize { get; set; } = 0!;

        [StringLength(10)]
        public string SecondLevelType { get; set; } = null!;


        public byte? ThirdLevelSize { get; set; }

        [StringLength(10)]
        public string? ThirdLevelType { get; set; }


        public byte? FourthLevelSize { get; set; }

        [StringLength(10)]
        public string? FourthLevelType { get; set; }


        public byte? FifthLevelSize { get; set; }

        [StringLength(10)]
        public string? FifthLevelType { get; set; }


        public byte? SixthLevelSize { get; set; }

        [StringLength(10)]
        public string? SixthLevelType { get; set; }
    }
}
