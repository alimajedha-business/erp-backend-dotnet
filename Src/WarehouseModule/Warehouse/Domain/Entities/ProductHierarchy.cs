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
    [Table("ProductHierarchy", Schema = "warehouse")]
    [Index("CompanyId", Name = "ProductHierarchy_CompanyId")]
    public class ProductHierarchy : BaseEntity
    {
        public required byte FirstLevelSize { get; set; }

        [StringLength(10)]
        public required string FirstLevelType { get; set; }

        public required byte SecondLevelSize { get; set; }

        [StringLength(10)]
        public required string SecondLevelType { get; set; }

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

        public byte? SeventhLevelSize { get; set; }

        [StringLength(10)]
        public string? SeventhLevelType { get; set; }
    }
}
