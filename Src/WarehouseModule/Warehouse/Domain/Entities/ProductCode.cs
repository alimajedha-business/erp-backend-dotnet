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
    [Table("ProductCodes", Schema = "Warehouse")]
    public class ProductCode
    {
        [Key]
        public int Id { get; set; }

        public int FirstLevelCode { get; set; } = 0!;

        [StringLength(100)]
        public string FirstLevelName { get; set; } = null!;


        public int SecondLevelCode { get; set; } = 0!;

        [StringLength(100)]
        public string SecondLevelName { get; set; } = null!;


        public int? ThirdLevelCode { get; set; }

        [StringLength(100)]
        public string? ThirdLevelName { get; set; }

        public bool? ThirdNextLevel { get; set; }


        public int? FourthLevelCode { get; set; }

        [StringLength(100)]
        public string? FourthLevelName { get; set; }

        public bool? FourthNextLevel { get; set; }


        public int? FifthLevelCode { get; set; }

        [StringLength(100)]
        public string? FifthLevelName { get; set; }

        public bool? FifthNextLevel { get; set; }


        public int? SixthLevelCode { get; set; }

        [StringLength(100)]
        public string? SixthLevelName { get; set; }

        public bool? SixthNextLevel { get; set; }


        public int? SeventhLevelCode { get; set; }

        [StringLength(100)]
        public string? SeventhLevelName { get; set; }

        public bool? SeventhNextLevel { get; set; }

    }
}
