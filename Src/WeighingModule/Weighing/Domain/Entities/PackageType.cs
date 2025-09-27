using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Weighing.Domain.Common;

namespace Weighing.Domain.Entities
{
    /// <summary>
    /// نوع بسته
    /// </summary>
    [Table("PackageType", Schema = "Weighing")]
    public partial class PackageType: BaseEntity
    {
        [StringLength(10)]
        public string Code { get; set; } = null!;
        
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(250)]
        public string? Description { get; set; }

       

    }
}
