using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Entities
{
    [Table("ProductCode", Schema = "warehouse")]
    [Index("CompanyId", Name = "ProductCode_CompanyId")]
    public class ProductCode:BaseEntity
    {
        public required int FirstLevelCode { get; set; }

        [StringLength(100)]
        public required string FirstLevelName { get; set; }

        public required int SecondLevelCode { get; set; }

        [StringLength(100)]
        public required string SecondLevelName { get; set; }

        public bool ThirdNextLevel { get; set; }
        
        public int? ThirdLevelCode { get; set; }

        [StringLength(100)]
        public string? ThirdLevelName { get; set; }

        public bool FourthNextLevel { get; set; }

        public int? FourthLevelCode { get; set; }

        [StringLength(100)]
        public string? FourthLevelName { get; set; }

        public bool FifthNextLevel { get; set; }

        public int? FifthLevelCode { get; set; }

        [StringLength(100)]
        public string? FifthLevelName { get; set; }

        public bool SixthNextLevel { get; set; }
        
        public int? SixthLevelCode { get; set; }

        [StringLength(100)]
        public string? SixthLevelName { get; set; }

        public bool SeventhNextLevel { get; set; }
        
        public int? SeventhLevelCode { get; set; }

        [StringLength(100)]
        public string? SeventhLevelName { get; set; }

    }
}
