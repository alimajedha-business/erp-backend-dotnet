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
    [Table("package_types", Schema = "weighing")]
    public partial class PackageType: BaseEntity
    {
      

        [Column("code")]
        [StringLength(10)]
        public string Code { get; set; } = null!;

        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Column("description")]
        [StringLength(250)]
        public string? Description { get; set; }

       

    }
}
